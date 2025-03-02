// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Modules;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Framework.ModuleZ.Implementations;
using Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.ModelBuilder;

[ExcludeFromCodeCoverage]
public class ControllerServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(RequestContext).Assembly)
            .AddOData()
            .AddJsonOptions(opt =>
            {
                // Default enum serialization on return to a string
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
    }
}