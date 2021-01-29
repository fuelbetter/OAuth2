
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Authmanagement.Context
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity=> { entity.ToTable(name: "FuelbetterUser"); });
            builder.Entity<IdentityRole>(entity => {entity.ToTable(name: "FuelbetterUserRoles");});
        }

        
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientScope> ClientScopes { get; set; }
        public DbSet<ClientSecret> ClientSecrets { get; set; }
        public DbSet<ClientGrantType> ClientGrantTypes { get; set; }
        public DbSet<ClientClaim> ClientClaims { get; set; }
      

        public DbSet<ApiResource> ApiResources { get; set; }
        public DbSet<ApiResourceSecret> ApiResourceSecrets { get; set; }
        public DbSet<ApiResourceClaim> ApiResourceClaims { get; set; }
        public DbSet<ApiResourceScope> ApiResourceScopes { get; set; }
        public DbSet<ApiResourceProperty> ApiResourceProperties { get; set; }
        

        public DbSet<ApiScope> ApiScopes { get; set; }
        public DbSet<ApiScopeClaim> ApiScopeClaims { get; set; }
        public DbSet<ApiScopeProperty> ApiScopeProperties { get; set; }
        public DbSet<ApiSecret> ApiSecrets { get; set; }
        public DbSet<IdentityResource> IdentityResources { get; set; }
        public DbSet<IdentityResourceClaim> IdentityResourceClaims { get; set; }
        public DbSet<IdentityResourceProperty> IdentityResourceProperties { get; set; }

    }
}
