// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Modules;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Framework.ModuleZ.Implementations;
using Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
public class ControllerServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(RequestContext).Assembly)
            .AddJsonOptions(opt =>
            {
                // Default enum serialization on return to a string
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
    }
}