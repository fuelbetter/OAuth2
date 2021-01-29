using Authmanagement.Proxy.resources.Dtos;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authmanagement.Proxy.resources
{
    public class ResourceFactory : IResourceFactory
    {
        public List<ApiResource> ApiResources()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// JB. Create new Protected Resource
        /// </summary>
        /// <param name="dto">Binding Data Transfer Object</param>
        /// <returns></returns>
        public ApiResource BuildApiResource(ResourceBindingDto dto)
        {
            return new ApiResource
            {
                Description = dto.Description,
                Name = dto.Name,
                DisplayName = dto.DisplayName,
                Enabled = true
            };
        }

        public ApiResource PatchApiResource(int resourceId)
        {
            throw new NotImplementedException();
        }
    }
}
