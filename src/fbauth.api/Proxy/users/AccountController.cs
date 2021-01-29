using authmanagement.Proxy.users.Dtos.BindingDtos;
using Authmanagement.Context;
using Authmanagement.Logging;
using Authmanagement.Proxy.users.Dtos.BindingDtos;
using Authmanagement.Proxy.users.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

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
        public AccountController(ILoggerManager logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, ApplicationDbContext userContext)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userContext = userContext;
        }
        [HttpPost]
        [Route("register")]
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

        [HttpPost, Route("Login")]
        [AllowAnonymous]
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

                    //JB. Add claims on the fly (change to read those from Db)
                    var claims = new List<Claim> {
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim("Audience","Fuelbetter")
                    };

                    var accessToken = _tokenService.GenerateAccessToken(claims);
                    //var refreshToken = _tokenService.GenerateRefreshToken();

                    //user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

                    //JB. Save the new Refresh Token
                    _userContext.SaveChanges();

                    _logger.LogInfo("User logged in.");

                    //var tokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);
                    return Ok(new { Token = accessToken }); //, RefreshToken = refreshToken});
                }
                //if (result.RequiresTwoFactor)
                //{
                //    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                //}
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
            // If we got this far, something failed, redisplay form
            return Unauthorized();
        }
    }
}
