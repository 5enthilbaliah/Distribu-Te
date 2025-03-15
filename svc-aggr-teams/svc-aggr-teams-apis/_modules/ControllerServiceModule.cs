// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Teams.Apis.Modules;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Associates;
using Application.Base;
using Application.SquadAssociates;
using Application.Squads;
using Framework.ModuleZ.Implementations;
using Framework.OData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Odata;

[ExcludeFromCodeCoverage]
public class ControllerServiceModule : DependencyServiceModule
{
    static IEdmModel GetAggregateEdmModel()
    {
        var odataBuilder = new ODataConventionModelBuilder();
        odataBuilder.EntitySet<AssociateModel>("associates");
        odataBuilder.EntitySet<SquadModel>("squads");
        odataBuilder.EntitySet<SquadAssociateModel>("squad-associates");
        
        odataBuilder.AddOdataConfigurations<AssociateElementConfiguration, AssociateElement>();
        odataBuilder.AddOdataConfigurations<SquadElementConfiguration, SquadElement>();
        odataBuilder.AddOdataConfigurations<SquadAssociateElementConfiguration, SquadAssociateElement>();
        
        odataBuilder.AddOdataConfigurations<AssociateModelConfiguration, AssociateModel>();
        odataBuilder.AddOdataConfigurations<SquadModelConfiguration, SquadModel>();
        odataBuilder.AddOdataConfigurations<SquadAssociateModelConfiguration, SquadAssociateModel>();
        return odataBuilder.GetEdmModel();
    }

    static IEdmModel GetEdmModel()
    {
        var odataBuilder = new ODataConventionModelBuilder();
        odataBuilder.EntitySet<AssociateElement>("associates");
        odataBuilder.EntitySet<SquadElement>("squads");
        odataBuilder.EntitySet<SquadAssociateElement>("squad-associates");
        
        odataBuilder.AddOdataConfigurations<AssociateElementConfiguration, AssociateElement>();
        odataBuilder.AddOdataConfigurations<SquadElementConfiguration, SquadElement>();
        odataBuilder.AddOdataConfigurations<SquadAssociateElementConfiguration, SquadAssociateElement>();
        return odataBuilder.GetEdmModel();
    }
    
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment,
        IConfiguration configuration)
    {
        services.AddControllers(config =>
            {
                // clear default model validation - handle this in application layer
                config.ModelValidatorProviders.Clear();
            }).AddApplicationPart(typeof(ControllerServiceModule).Assembly)
            .AddJsonOptions(opt =>
            {
                // Default enum serialization on return to a string
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                opt.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.SnakeCaseLower;
            }).AddOData(opt =>
            {
                opt.Select().Filter().OrderBy().Expand().Count().SetMaxTop(500)
                    .AddRouteComponents("protected", GetEdmModel())
                    .AddRouteComponents("protected/aggregates", GetAggregateEdmModel());
            });
    }
}