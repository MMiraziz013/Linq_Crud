using Clean.Application.Abstractions;
using Clean.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DataContext>(opt => opt.UseNpgsql(connectionString));
        services.AddScoped<IDataContext>(provider => provider.GetRequiredService<DataContext>());

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ITaskReadService, TaskReadService>();
        services.AddTransient<ITaskRepository, TaskRepository>();
        services.AddTransient<IProjectRepository, ProjectRepository>();
        services.AddTransient<ITaskAssignmentRepository, TaskAssignmentRepository>();
        return services;
    }
}