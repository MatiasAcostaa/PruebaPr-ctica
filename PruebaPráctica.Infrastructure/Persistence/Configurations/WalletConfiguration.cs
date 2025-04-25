using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaPráctica.Domain.Entities;

namespace PruebaPráctica.Infrastructure.Persistence.Configurations;

public sealed class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.HasKey(w => w.Id);
        builder.Property(w => w.DocumentId)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(w => w.Name)
            .HasMaxLength(100);
        builder.Property(w => w.Balance)
            .HasPrecision(14,2);
    }
}