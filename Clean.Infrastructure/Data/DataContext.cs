using Clean.Application.Abstractions;
using Clean.Domain.Entities;
using Clean.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Clean.Infrastructure.Data;

public class DataContext : DbContext, IDataContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Domain.Entities.Task> Tasks { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<TaskAssignment> TaskAssignments { get; set; }

    public async Task MigrateAsync()
    {
        await Database.MigrateAsync();
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return base.SaveChangesAsync(ct);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskAssignmentConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectConfiguration).Assembly);
    }
}