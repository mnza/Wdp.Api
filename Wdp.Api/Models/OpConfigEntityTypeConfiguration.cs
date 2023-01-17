using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wdp.Api.Models
{
    public class OpConfigEntityTypeConfiguration : IEntityTypeConfiguration<OpConfig>
    {
        public void Configure(EntityTypeBuilder<OpConfig> builder)
        {
            builder.ToTable("op_config").HasKey(b => new { b.OperatorId, b.Category, b.Key });
            builder.Property(b => b.OperatorTime).HasColumnName("operator_time").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(b => b.OperatorId).HasColumnName("operator_id");
            builder.Property(b => b.Remark).HasMaxLength(200);
            builder.Property(b => b.Key).HasMaxLength(40);
            builder.Property(b => b.Value).HasMaxLength(200);
            builder.Property(b=>b.Category).HasMaxLength(20);
        }
    }
}
