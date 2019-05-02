using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Karnak.Domain.Models;

namespace Karnak.Infra.Data.Mappings
{    
    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Amount).HasColumnType("decimal(5, 2)");

            builder.Property(e => e.TransactionDate).HasColumnType("date");

            builder.HasOne(d => d.IdCardNavigation)
                .WithMany(p => p.Transaction)
                .HasForeignKey(d => d.IdCard)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_Card");

            builder.HasOne(d => d.IdTransactionStatusNavigation)
                .WithMany(p => p.Transaction)
                .HasForeignKey(d => d.IdTransactionStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_TransactionStatus");

            builder.HasOne(d => d.IdTransactionTypeNavigation)
                .WithMany(p => p.Transaction)
                .HasForeignKey(d => d.IdTransactionType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_TransactionType");
        }
    }
}
