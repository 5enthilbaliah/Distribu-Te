// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Modules;

using System.Diagnostics.CodeAnalysis;
using Asp.Versioning;
using Framework.ModuleZ.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal class ApiVersionServiceModule : DependencyServiceModule
{
    [ExcludeFromCodeCoverage]
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddApiVersioning(cfg =>
        {
            cfg.DefaultApiVersion = new ApiVersion(1, 0);
            cfg.AssumeDefaultVersionWhenUnspecified = true;
            cfg.ReportApiVersions = true;
            cfg.ApiVersionReader = new HeaderApiVersionReader("x-version");
        });
    }
}