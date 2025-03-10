using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Infrastructure.Identity.Models;
using Infrastructure.Tenancy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contexts
{
    public abstract class BaseDbContext : MultiTenantIdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        string,
        IdentityUserClaim<string>,
        IdentityUserRole<string>,
        IdentityUserLogin<string>,
        ApplicationRoleCLaim,
        IdentityUserToken<string>
        >
    {
        private new SchoolTenantInfo TenantInfo { get; set; }

        protected BaseDbContext(IMultiTenantContextAccessor<SchoolTenantInfo> tenantContextAccessor, DbContextOptions options)
                : base(tenantContextAccessor, options)
        {
            TenantInfo = tenantContextAccessor.MultiTenantContext.TenantInfo;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!string.IsNullOrEmpty(TenantInfo?.ConnectionString))
            {
                optionsBuilder.UseSqlServer(TenantInfo.ConnectionString, options => 
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
