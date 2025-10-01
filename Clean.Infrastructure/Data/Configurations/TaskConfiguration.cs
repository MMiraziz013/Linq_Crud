using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Clean.Domain.Entities.Task;

namespace Clean.Infrastructure.Data.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
    {
        builder.ToTable("tasks");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Title).IsRequired().HasMaxLength(50);
        builder.Property(b => b.Description).HasMaxLength(200);
        builder.Property(b => b.CreatedDate).IsRequired().HasDefaultValueSql("NOW()");
        builder.Property(b => b.DueDate).IsRequired(false);
        builder.HasOne(b => b.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId);
        builder.HasOne(t => t.CreatedUser)
            .WithMany(u => u.CreatedTasks);
        builder.HasMany(t => t.Assignments)
            .WithOne(a => a.Task)
            .HasForeignKey(t=> t.TaskId);
    }
}