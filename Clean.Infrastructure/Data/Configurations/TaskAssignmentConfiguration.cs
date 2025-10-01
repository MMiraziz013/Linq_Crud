using Clean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Infrastructure.Data.Configurations;

public class TaskAssignmentConfiguration : IEntityTypeConfiguration<TaskAssignment>
{
    public void Configure(EntityTypeBuilder<TaskAssignment> builder)
    {
        builder.ToTable("task_assignments");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.AssignedDate)
            .IsRequired();

        builder.HasOne(a => a.Task)
            .WithMany(t => t.Assignments)
            .HasForeignKey(a => a.TaskId);

        builder.HasOne(a => a.User)
            .WithMany(u => u.Assignments)
            .HasForeignKey(a => a.UserId);
    }
}