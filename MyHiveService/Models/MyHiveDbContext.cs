using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyHiveService.Services.Tenant;

namespace MyHiveService.Models
{
    public class MyHiveDbContext : DbContext
    {
        private readonly ITenantProvider _tenantProvider;

        public MyHiveDbContext(DbContextOptions<MyHiveDbContext> options, ITenantProvider tenantProvider)
            : base(options)
        {
            _tenantProvider = tenantProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Hive>().HasQueryFilter(t => t.tenantId == _tenantProvider.GetTenantId());
            modelBuilder.Entity<HivePart>().HasQueryFilter(t => t.tenantId == _tenantProvider.GetTenantId());
            modelBuilder.Entity<Frame>().HasQueryFilter(t => t.tenantId == _tenantProvider.GetTenantId());
            modelBuilder.Entity<HiveInspection>().HasQueryFilter(t => t.tenantId == _tenantProvider.GetTenantId());
            modelBuilder.Entity<HivePartInspection>().HasQueryFilter(t => t.tenantId == _tenantProvider.GetTenantId());
            modelBuilder.Entity<FrameInspection>().HasQueryFilter(t => t.tenantId == _tenantProvider.GetTenantId());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Hive> Hives { get; set; }
        public DbSet<HiveInspection> HiveInspections { get; set; }
        public DbSet<HivePart> HiveParts { get; set; }
        public DbSet<HivePartInspection> HivePartInspections { get; set; }
        public DbSet<Frame> Frames { get; set; }
        public DbSet<FrameInspection> FrameInspections { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _setLastModified();
            return base.SaveChangesAsync(cancellationToken);
        }


        public override int SaveChanges()
        {
            _setLastModified();
            return base.SaveChanges();
        }

        private void _setLastModified()
        {
            var changes = from e in ChangeTracker.Entries()
                          where (e.State == EntityState.Modified || e.State == EntityState.Added) &&
                          e.Entity is ModelBase
                          select e;
            if (changes.Any())
            {
                foreach (var change in changes)
                {
                    (change.Entity as ModelBase).lastModified = DateTime.UtcNow;
                    (change.Entity as ModelBase).tenantId = _tenantProvider.GetTenantId();
                }
            }
        }
    }
}