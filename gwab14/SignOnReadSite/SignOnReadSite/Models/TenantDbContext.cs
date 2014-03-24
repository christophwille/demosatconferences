using System;
using System.Data.Entity;

namespace SignOnReadSite.Models
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext()
            : base("DefaultConnection")
        {
        }

        // Disable code-first if running against non-code-first database
        public static void DisableMigrations()
        {
            Database.SetInitializer<TenantDbContext>(new NullDatabaseInitializer<TenantDbContext>());
        }

        public DbSet<IssuingAuthorityKey> IssuingAuthorityKeys { get; set; }

        public DbSet<Tenant> Tenants { get; set; }
    }
}