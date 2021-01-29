using IdentityServer4.EntityFramework.Entities;

namespace Authmanagement.Proxy.resources
{
    public interface IResourceRepository
    {
        int CreateApiResource(ApiResource resource);
    }
}
