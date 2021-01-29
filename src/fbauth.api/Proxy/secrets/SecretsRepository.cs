using Authmanagement.Context;
using IdentityServer4.EntityFramework.Entities;
using System;

namespace Authmanagement.Proxy.secrets
{
    public class SecretsRepository:ISecretsRepository
    {
        private ApplicationDbContext _ctx;
        public SecretsRepository(ApplicationDbContext context)
        {
            _ctx = context;
        }

        public string AddApiSecret(ApiSecret model)
        {
            string result;
            try
            {
                using (/*var*/ _ctx /*= new ResourceConfigDbContext()*/)
                {
                    _ctx.ApiSecrets.Add(model);
                    _ctx.SaveChanges();
                    //int Id = 
                    result = model.Id.ToString();
                }

            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }
            return result;
        }

        public string AddClientSecret(ClientSecret model)
        {
            string result;
            try
            {
                using (_ctx)
                {
                    _ctx.ClientSecrets.Add(model);
                    _ctx.SaveChanges();
                    result = model.Id.ToString();
                }

            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }
            return result;
        }
    }
}
