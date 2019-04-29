using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Karnak.Domain.Models;

namespace Karnak.Infra.Data.Mappings
{    
    public class CardMap : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.CardNumber)
                .IsRequired()
                .HasMaxLength(19)
                .IsUnicode(false);

            builder.Property(e => e.ExpirationDate).HasColumnType("datetime");

            builder.Property(e => e.Limit).HasColumnType("decimal(5, 2)");

            builder.Property(e => e.Password)
                .IsRequired()
                .IsUnicode(false);

            builder.HasOne(d => d.IdBrandNavigation)
                .WithMany(p => p.Card)
                .HasForeignKey(d => d.IdBrand)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Card_CardBrand");

            builder.HasOne(d => d.IdCardTypeNavigation)
                .WithMany(p => p.Card)
                .HasForeignKey(d => d.IdCardType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Card_CardType");

            builder.HasOne(d => d.IdCustomerNavigation)
                .WithMany(p => p.Card)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Card_Customer");
        }
    }
}
