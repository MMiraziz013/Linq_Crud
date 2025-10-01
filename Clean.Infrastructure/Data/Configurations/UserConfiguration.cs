using Clean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.RegistrationDate)
            .IsRequired().HasDefaultValueSql("NOW()");

        builder.HasMany(u => u.CreatedTasks)
            .WithOne(t => t.CreatedUser)
            .HasForeignKey(t => t.CreatedUserId);

        builder.HasMany(u => u.Assignments)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);
    }
}
