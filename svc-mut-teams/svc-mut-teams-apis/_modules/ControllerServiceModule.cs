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

[ExcludeFromCodeCoverage]
public class ControllerServiceModule : DependencyServiceModule
{
    private IEdmModel GetEdmModel()
    {
        var odataBuilder = new ODataConventionModelBuilder();
        odataBuilder.EntitySet<AssociateVm>("associates");
        odataBuilder.EntitySet<SquadVm>("squads");
        odataBuilder.EntitySet<SquadAssociateVm>("squad-associates");
            
        odataBuilder.EntityType<SquadAssociateVm>()
            .HasKey(x => new { x.SquadId, x.AssociateId });

        return odataBuilder.GetEdmModel();
    }
    
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(RequestContext).Assembly)
            .AddOData(opt =>
            {
                opt.Select()
                    .AddRouteComponents("odata/protected", GetEdmModel());

            }).AddJsonOptions(opt =>
            {
                // Default enum serialization on return to a string
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
    }
}