using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wdp.Api.Models
{
    public class BillDetailEntityTypeConfiguration : IEntityTypeConfiguration<BillDetail>
    {
        public void Configure(EntityTypeBuilder<BillDetail> builder)
        {
            builder.ToTable("bill_detail").HasKey(b => b.DetailId);
            builder.Property(b => b.DetailId).HasColumnName("detail_id").ValueGeneratedOnAdd();
            builder.Property(b => b.ItemType).HasColumnName("item_type").HasMaxLength(20).IsRequired();
            builder.Property(b => b.ItemName).HasColumnName("item_name").HasMaxLength(40).IsRequired();
            builder.Property(b => b.Remark).HasMaxLength(200).IsRequired(false);
            builder.Property(b => b.Amount).IsRequired();
            builder.Property(b => b.Price).IsRequired();
            builder.Property(b => b.BillDateTime).HasColumnName("bill_date_time").HasColumnType("datetime");
            builder.Property(b => b.OperatorTime).HasColumnName("operator_time").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");

            
            builder.HasOne<BillMaster>(b => b.BillMaster).WithMany(c => c.BillDetails);
        }
    }
}
