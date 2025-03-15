// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Modules;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Associates.DataContracts;
using Application.SquadAssociates.DataContracts;
using Application.Squads.DataContracts;
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
internal class ControllerServiceModule : DependencyServiceModule
{
    static IEdmModel GetEdmModel()
    {
        var odataBuilder = new ODataConventionModelBuilder();
        odataBuilder.EntitySet<AssociateResponse>("associates");
        odataBuilder.EntitySet<SquadResponse>("squads");
        odataBuilder.EntitySet<SquadAssociateResponse>("squad-associates");
        
        odataBuilder.AddOdataConfigurations<AssociateResponseConfiguration, AssociateResponse>();
        odataBuilder.AddOdataConfigurations<SquadResponseConfiguration, SquadResponse>();
        odataBuilder.AddOdataConfigurations<SquadAssociateResponseConfiguration, SquadAssociateResponse>();
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
                opt.Select()
                    .AddRouteComponents("protected", GetEdmModel());
            });
    }
}