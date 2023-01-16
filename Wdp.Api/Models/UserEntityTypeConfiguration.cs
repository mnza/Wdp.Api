using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wdp.Api.Models
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("users").HasKey(u => u.UserId);
            //主键自增
            builder.Property(b => b.UserId).HasColumnName("user_id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(b => b.UserName).HasColumnName("user_name").HasMaxLength(40);
            builder.Property(b => b.IdNo).HasColumnName("id_no").HasMaxLength(18);
            builder.Property(b => b.EMail).HasMaxLength(50);
            builder.Property(b => b.Nickname).HasMaxLength(50);
            builder.Property(b => b.PhoneNumber).HasColumnName("phone_number").HasMaxLength(20);
        }
    }
}
