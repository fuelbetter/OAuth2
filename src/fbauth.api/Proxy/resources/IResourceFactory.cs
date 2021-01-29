using Authmanagement.Proxy.resources.Dtos;
using IdentityServer4.EntityFramework.Entities;
using System.Collections.Generic;

namespace Authmanagement.Proxy.resources
{
    public interface IResourceFactory
    {
        ApiResource BuildApiResource(ResourceBindingDto dto);
        List<ApiResource> ApiResources();
        ApiResource PatchApiResource(int resourceId);
    }
}
