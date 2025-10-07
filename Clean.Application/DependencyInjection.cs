using Clean.Application.Abstractions;
using Clean.Application.Services;
using Clean.Application.Services.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ITaskService, TaskService>();
        services.AddTransient<IProjectService, ProjectService>();
        services.AddTransient<ITaskAssignmentService, TaskAssignmentService>();
        
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        return services;
    }
}