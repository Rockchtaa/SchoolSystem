using Finbuckle.MultiTenant.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tenancy
{
    public class SchoolTenantInfo : ITenantInfo
    {
        public string Id { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public string Email { get; set; }
        public string Firsttname { get; set; }
        public string Lastname { get; set; }
        public DateTime ValidUntil { get; set; }
        public bool IsEnabled { get; set; }
    }
}
