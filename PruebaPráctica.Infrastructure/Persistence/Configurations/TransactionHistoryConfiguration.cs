using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaPráctica.Domain.Entities;

namespace PruebaPráctica.Infrastructure.Persistence.Configurations;

public sealed class TransactionHistoryConfiguration : IEntityTypeConfiguration<TransactionHistory>
{
    public void Configure(EntityTypeBuilder<TransactionHistory> builder)
    {
        builder.HasKey(th => th.Id);
        builder.Property(th => th.WalletId).IsRequired();
        builder.Property(th => th.Amount).HasPrecision(14,2);
        builder.HasOne(th => th.Wallet)
            .WithMany(w => w.TransactionHistory)
            .HasForeignKey(th => th.WalletId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}