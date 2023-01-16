using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wdp.Api.Models
{
    public class FavWebsiteEntityTypeConfiguration : IEntityTypeConfiguration<FavWebsite>
    {
        public void Configure(EntityTypeBuilder<FavWebsite> builder)
        {
            builder.ToTable("fav_websites");
            builder.HasKey(b => b.Id);
            //主键自增
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Property(b => b.Name).HasMaxLength(20).IsRequired();
            builder.Property(b => b.Url).HasMaxLength(200).IsRequired();
            builder.Property(b => b.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(b => b.Category).HasMaxLength(20);
            builder.Property(b => b.Icon).HasMaxLength(200);
        }
    }
}
