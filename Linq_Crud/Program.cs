using System.Net;
using Clean.Application;
using Clean.Application.Abstractions;
using Clean.Infrastructure;
using Clean.Infrastructure.Data;
using Linq_Crud.Middlewares;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Linq_Crud;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Configurations of the Serilog before app builds
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Host.UseSerilog();
        builder.Services.AddControllers();
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        
        builder.Services.AddTransient<LoggingMiddleware>();
        
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IDataContext>();
            await db.MigrateAsync();
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();


            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty; // Set the route prefix to an empty string
            });

            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/error");
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();


        app.MapControllers();

        app.Map("/error", (HttpContext context) =>
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;

            var statusCode = (int)HttpStatusCode.InternalServerError;
            var title = "An unexpected error occurred.";

            if (exception is ArgumentException ||
                exception is System.ComponentModel.DataAnnotations.ValidationException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                title = "Invalid input.";
            }
            else if (exception is ArgumentOutOfRangeException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
                title = "The requested resource was not found.";
            }

            return Results.Problem(
                title: title,
                statusCode: statusCode,
                detail: exception?.Message
            );
        });

        await app.RunAsync();
    }
}