using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.Models;
using ITBees.AccessControl.Services;
using ITBees.AccessControl.Services.Common;
using ITBees.AccessControl.Services.PlatformAdmin;
using ITBees.AccessControl.Services.PlatformOperator;
using ITBees.Models.Hardware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITBees.AccessControl.Setup
{
    public class AccessControlSetup
    {
        public static void Register<TContext, TIdentityUser, TAccessCard>(IServiceCollection services,
            IConfigurationRoot configurationRoot) where TContext : DbContext
            where TIdentityUser : IdentityUser
            where TAccessCard : AccessCard, new()
        {
            services.AddScoped<IAccessControlSignalReceived, AccessControlSignalReceived>();
            services.AddScoped<IUnauthorizedRfidDevicesService, UnauthorizedRfidDevicesService>();
            services.AddScoped<IAuthorizeRfidDeviceService, AuthorizeDeviceService>();
            services.AddScoped<IAuthorizeAccessCardsService, AuthorizeAccessCardsService>();
            services.AddScoped<IAllowedCardsService, AllowedCardsService>();
            services.AddScoped<IAccessCardsService, AccessCardsService>();
            services.AddScoped<IAccessCardTypesService, AccessCardTypesService>();
            services.AddScoped<IOperatorCompaniesService, OperatorCompaniesService>();
            services.AddScoped<IUnauthorizedAccessCardLogsService, UnauthorizedAccessCardLogsService>();
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IAcGroupsService, AcGroupsService>();
            services.AddScoped<IAcGroupAccessCardsService, AcGroupAccessCardsService>();
            services.AddScoped<IAccessCardStatusService, AccessCardStatusService>();
        }
    }

    public class DbModelBuilder
    {
        public static void Register(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcGroup>().HasKey(x => x.Guid);
            modelBuilder.Entity<AcGroupAccessCards>().HasKey(x => new { x.AcGroupGuid, x.AccessCardGuid });
            modelBuilder.Entity<AccessCardType>().HasKey(x => x.Id);
            modelBuilder.Entity<AccessCard>().HasKey(x => x.Guid);
            modelBuilder.Entity<AccessCard>().HasIndex(x => x.CardId).IsUnique();
            modelBuilder.Entity<AccessCardGroup>().HasKey(x => x.Guid);
            modelBuilder.Entity<AccessCardCardGroup>().HasKey(x => x.Guid);
            modelBuilder.Entity<AllowedAccessCard>().HasKey(x => x.Guid);
            modelBuilder.Entity<AllowedAccessCard>().HasIndex(x => x.CardId).IsUnique();
            modelBuilder.Entity<RfidReaderDevice>().HasKey(x => x.Guid);
            modelBuilder.Entity<RfidReaderDevice>().HasIndex(x => x.Mac).IsUnique();
            modelBuilder.Entity<UnauthorizedAccessCardLog>().HasKey(x => x.Id);
            modelBuilder.Entity<UnauthorizedRfidDevice>().HasKey(x => x.Guid);
            modelBuilder.Entity<UnauthorizedRfidDevice>().HasIndex(x => x.Mac).IsUnique();
            //modelBuilder.Entity<DeviceHub>().HasDiscriminator<string>("DeviceType")
            //    .HasValue<PhysicalDeviceHub>(nameof(PhysicalDeviceHub) + "Type");
        }
    }
}
