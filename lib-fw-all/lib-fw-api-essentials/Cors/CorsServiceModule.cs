namespace DistribuTe.Framework.ApiEssentials.Cors;

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModuleZ.Implementations;

public class CorsServiceModule : DependencyServiceModule
{
    [ExcludeFromCodeCoverage]
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddCors(options => options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));
    }
}