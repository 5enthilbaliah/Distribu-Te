// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Modules;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Associates.Models;
using Application.SquadAssociates.Models;
using Application.Squads.Models;
using Framework.ModuleZ.Implementations;
using Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OData.ModelBuilder.Config;
using Odata;

[ExcludeFromCodeCoverage]
public class ControllerServiceModule : DependencyServiceModule
{
    static IEdmModel GetEdmModel()
    {
        var odataBuilder = new ODataConventionModelBuilder();
        odataBuilder.EntitySet<AssociateVm>("associates");
        odataBuilder.EntitySet<SquadVm>("squads");
        odataBuilder.EntitySet<SquadAssociateVm>("squad-associates");
        
        odataBuilder.AddOdataConfigurations<AssociateVmConfiguration, AssociateVm>();
        odataBuilder.AddOdataConfigurations<SquadVmConfiguration, SquadVm>();
        odataBuilder.AddOdataConfigurations<SquadAssociateVmConfiguration, SquadAssociateVm>();
        return odataBuilder.GetEdmModel();
    }
    
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(RequestContext).Assembly)
            .AddJsonOptions(opt =>
            {
                // Default enum serialization on return to a string
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                opt.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.SnakeCaseLower;
            }).AddOData(opt =>
            {
                opt.Select()
                    .AddRouteComponents("odata/protected", GetEdmModel());
            });
    }
}