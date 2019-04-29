using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Karnak.Domain.Models;

namespace Karnak.Infra.Data.Mappings
{    
    public class TransactionStatusMap : IEntityTypeConfiguration<TransactionStatus>
    {
        public void Configure(EntityTypeBuilder<TransactionStatus> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
