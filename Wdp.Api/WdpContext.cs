using Microsoft.EntityFrameworkCore;
using Wdp.Api.Models;

namespace Wdp.Api
{
    public class WdpContext : DbContext
    {
        public DbSet<FavWebsite> FavWebsites { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OpConfig> OpConfigs { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<BillMaster> BillMasters { get; set; }

        public WdpContext(DbContextOptions<WdpContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new FavWebsiteEntityTypeConfiguration().Configure(modelBuilder.Entity<FavWebsite>());
            new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
            new BillDetailEntityTypeConfiguration().Configure(modelBuilder.Entity<BillDetail>());
            new OpConfigEntityTypeConfiguration().Configure(modelBuilder.Entity<OpConfig>());
            new BillMasterEntityTypeConfiguration().Configure(modelBuilder.Entity<BillMaster>());
        }
    }
}
