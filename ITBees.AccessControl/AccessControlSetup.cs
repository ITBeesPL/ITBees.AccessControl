
using ITBees.FAS.Setup;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITBees.AccessControl
{
    public class AccessControlSetup : IFasDependencyRegistrationWithGenerics
    {
        public void Register<TContext, TIdentityUser>(IServiceCollection services, IConfigurationRoot configurationRoot) where TContext : DbContext where TIdentityUser : IdentityUser, new()
        {
            throw new NotImplementedException();
        }
    }
}
