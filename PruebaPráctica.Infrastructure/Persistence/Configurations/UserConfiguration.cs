using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaPráctica.Domain.Entities;

namespace PruebaPráctica.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Username)
            .HasMaxLength(10);
        builder.Property(u => u.PasswordHash)
            .IsRequired();
    }
}