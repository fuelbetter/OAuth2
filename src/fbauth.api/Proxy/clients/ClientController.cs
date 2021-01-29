using Authmanagement.Context;
using Authmanagement.Proxy.clients.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authmanagement.Proxy.clients
{
    [Route("client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IClientFactory _factory;
        private IClientRepository _repo;

        public ClientController(IClientFactory factory, IClientRepository repo)
        {

            _factory = factory;
            _repo = repo;
        }
        /// <summary>
        /// Create a new Client and persist it in DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ClientErrorResponseDto))]

        [Produces("application/json")]
        //[Authorize]
        public async Task<IActionResult> AddClient(ClientBindingDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (await _repo.ClientExist(dto.ClientName))
                {
                    ClientErrorResponseDto response = new ClientErrorResponseDto { Error = "Conflict", Message = "A Client with name '" + dto.ClientName + "' already exist" };
                    return Conflict(response);
                }
                return Ok(await _repo.AddClient(_factory.CreateClientEntity(dto), dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
