// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Pipelines;

using Application.Associates.Models;
using Application.SquadAssociates.Models;
using Application.Squads.Models;
using Framework.ModuleZ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

public class MvcPipeline : IMiddlewarePipeline
{
    private IEdmModel GetEdmModel()
    {
        var odataBuilder = new ODataConventionModelBuilder();
        odataBuilder.EntitySet<AssociateVm>("associates");
        odataBuilder.EntitySet<SquadVm>("squads");
        odataBuilder.EntitySet<SquadAssociateVm>("squad-associates");

        return odataBuilder.GetEdmModel();
    }
    
    public void Setup(WebApplication app, IWebHostEnvironment environment, IConfiguration configuration)
    {
        app.UseRouting();
        app.MapControllers();
    }
}