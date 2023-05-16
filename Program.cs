using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using DotnetCoreMssql.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;


public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices((hostContext, services) =>
                {
                    services.AddControllers(); // Add controller services to the container

                    // Retrieve the connection string from the appsettings.json file
                    IConfiguration configuration = hostContext.Configuration;
                    string connectionString = configuration.GetConnectionString("DefaultConnection");

                    // Configure the DbContext with the retrieved connection string
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(connectionString)
                    );

                    // Register all classes in the Repositories namespace as scoped services
                    Assembly repositoryAssembly = Assembly.GetExecutingAssembly();
                    var repositoryTypes = repositoryAssembly.GetTypes()
                        .Where(type => type.Namespace == "DotnetCoreMssql.Repositories");
                    foreach (var repositoryType in repositoryTypes)
                    {
                        services.AddScoped(repositoryType);
                    }
                });
                webBuilder.Configure(app =>
                {
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers(); // Map controllers
                    });
                });
                // webBuilder.UseStartup<Startup>();
                webBuilder.UseUrls("http://localhost:" + GetPortFromEnvironment());
            });

    private static string GetPortFromEnvironment()
    {
        // Read the environment variable for the port, e.g., "ASPNETCORE_PORT"
        string port = Environment.GetEnvironmentVariable("ASPNETCORE_PORT") ?? "5000";
        return port;
    }
}