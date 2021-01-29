using Authmanagement.Proxy.resources.Dtos;
using Authmanagement.Proxy.secrets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authmanagement.Proxy.resources
{
    [Route("resource")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private ISecretsService _secrets;
        private ISecretsFactory _secretFactory;
        private IResourceFactory _resourceFactory;
        private IResourceRepository _resourceRepo;

        public ResourceController(ISecretsService secrets, IResourceRepository resourceRepo, IResourceFactory resourceFactory, ISecretsFactory secretFactory)
        {
            _secrets = secrets;
            _resourceFactory = resourceFactory;
            _resourceRepo = resourceRepo;
            _secretFactory = secretFactory;
        }

        /// <summary>
        /// Create a new ApiResource and Secret.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public IActionResult createResource(ResourceBindingDto dto)
        {
            int daId = _resourceRepo.CreateApiResource(_resourceFactory.BuildApiResource(dto));

            if (!string.IsNullOrEmpty(daId.ToString()))
            {
                //JB. Add Secret
                _secrets.AddSecret(_secretFactory.BuildApiSecretBinding(dto, daId, "ApiResource"));
            }
            return Ok("Success " + daId);
        }
    }
}
