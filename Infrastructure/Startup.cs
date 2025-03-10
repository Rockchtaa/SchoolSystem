using Finbuckle.MultiTenant;
using Infrastructure.Contexts;
using Infrastructure.Tenancy;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration )
        {
            return services.AddDbContext<TenantDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            })
            .AddMultiTenant<SchoolTenantInfo>()
            .WithHeaderStrategy(TenancyConstant.TenantIdName)
            .WithClaimStrategy(TenancyConstant.TenantIdName)
            .WithEFCoreStore<TenantDbContext, SchoolTenantInfo>()
            .Services
            .AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
        public  static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseMultiTenant();
            return app;
        }
    }
}

