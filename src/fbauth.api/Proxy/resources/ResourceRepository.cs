using Authmanagement.Context;
using IdentityServer4.EntityFramework.Entities;

namespace Authmanagement.Proxy.resources
{
    public class ResourceRepository : IResourceRepository
    {
        private ApplicationDbContext _ctx;
        public ResourceRepository(ApplicationDbContext context)
        {
            _ctx = context;
        }

        /// <summary>
        /// Add new ApiResource to Database.
        /// </summary>
        /// <param name="resource"></param>
        /// <returns>Int ID of newly created ApiResource</returns>
        public int CreateApiResource(ApiResource resource)
        {
            int daId;
            using (_ctx)
            {
                _ctx.ApiResources.Add(resource);
                _ctx.SaveChanges();
                daId = resource.Id;
            }
            return daId;
        }
    }
}
