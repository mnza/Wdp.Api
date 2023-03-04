using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wdp.Api.Models
{
    public class BillMasterEntityTypeConfiguration : IEntityTypeConfiguration<BillMaster>
    {
        public void Configure(EntityTypeBuilder<BillMaster> builder)
        {
            builder.ToTable("bill_master").HasKey(b => b.BillId);
            builder.Property(b => b.BillId).HasColumnName("bill_id");
            builder.Property(b => b.BillMoney).HasColumnName("bill_money");
            builder.Property(b => b.BillDateTime).HasColumnName("bill_date_time").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(b => b.BillName).HasColumnName("bill_name").IsRequired().HasMaxLength(50);
            builder.Property(b => b.Remark).IsRequired(false);
            builder.HasOne(b => b.User).WithMany();
        }
    }
}
