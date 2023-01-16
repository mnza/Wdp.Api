using Microsoft.EntityFrameworkCore;
using Wdp.Api.Models;

namespace Wdp.Api
{
    public class WdpContext : DbContext
    {
        public DbSet<FavWebsite> FavWebsites { get; set; }
        public DbSet<Users> Users { get; set; }

        public WdpContext(DbContextOptions<WdpContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavWebsite>().ToTable("fav_websites");

            new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<Users>());
        }
    }
}
