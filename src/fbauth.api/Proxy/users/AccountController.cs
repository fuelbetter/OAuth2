using authmanagement.Proxy.users.Dtos.BindingDtos;
using Authmanagement.Context;
using Authmanagement.Logging;
using Authmanagement.Proxy.users.Dtos.BindingDtos;
using Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Authmanagement.Proxy.tokens;

namespace authmanagement.Proxy.users
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _userContext;
        private readonly IFirebaseTokenService _firebaseToken;
        public AccountController(ILoggerManager logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, ApplicationDbContext userContext, IFirebaseTokenService firebaseToken)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userContext = userContext;
            _firebaseToken = firebaseToken;
        }
        [HttpPost,Route("register"),AllowAnonymous, Produces("application/json")]
        public async Task<IActionResult> Register([FromBody] UserRegister model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

            var user = new ApplicationUser { Email = model.Email, UserName = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInfo("User created a new account on " + DateTime.Now);
            }
            return Ok(result);
        }

        [HttpPost, Route("Login"), AllowAnonymous, Produces("application/json")]
        public async Task<IActionResult> Login([FromBody] LoginInput model)
        {

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
                if (result.Succeeded)
                {
                    //JB. Get the Entity of the that is Login in
                    var user = _userContext.Users.FirstOrDefault(u => u.Email == model.Email);

                    //JB. Add claims on the fly (must change it to be read from Db)
                    var claims = new List<Claim> {
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim("Audience","Fuelbetter")
                    };

                    var accessToken = _tokenService.GenerateAccessToken(claims);
                    
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

                    //JB. Save the new Refresh Token
                    _userContext.SaveChanges();

                    _logger.LogInfo("User logged in.");

                    //var tokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);
                    return Ok(new { Token = accessToken }); //, RefreshToken = refreshToken});
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarn("User account locked out.");
                    return BadRequest("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(model);
                }
            }
            // If we get this far, something failed, redisplay form
            return Unauthorized();
        }
        
        [HttpPost, Route("facebookLogin"), AllowAnonymous, Produces("application/json")]
        public async Task<IActionResult> FacebookLogin([FromBody] FacebookLoginInput model)
        {
            if (!ModelState.IsValid) {
                return BadRequest("Invalid input");
            }
            //JB. Below is just a Test meant to be moved to the corresponding service
            FacebookClient cl = new FacebookClient();
            dynamic result = cl.Get("oauth/access_token", new
            {
                client_id = "474747580070548",
                client_secret = "ef64686ec8533c6762d271b21d77f3e2",
                grant_type = "client_credentials"
            });

            cl.AccessToken = result.access_token;
            HttpClient client = new HttpClient();
            var tokenHandler = new JwtSecurityTokenHandler();
            string encodedJwt = model.FacebookCode;

            return Ok();
        }

        [HttpPost, Route("firebaselogin"), AllowAnonymous, Produces("application/json")]
        public async Task<IActionResult> FirebaseLogin([FromBody] FirebaseLoginInput model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }
            var claims = new List<Claim>
            {
                    new Claim("Audience","Fuelbetter")
                    };
            bool validationResult = await _firebaseToken.ValidateIdToken(model.FirebaseToken);
            if (validationResult)
            {
                var result = _tokenService.GenerateAccessToken(claims);
                return Ok(result);
            }
            return Unauthorized();
        }
    }
}
