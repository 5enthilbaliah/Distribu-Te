// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Projects.Apis.Odata;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.ProjectCategories.DataContracts;
using Application.Projects.DataContracts;
using Application.SquadProjects.DataContracts;
using Framework.ApiEssentials.Odata;
using Framework.ModuleZ.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

[ExcludeFromCodeCoverage]
internal class ControllerServiceModule : DependencyServiceModule
{
    static IEdmModel GetEdmModel()
    {
        var odataBuilder = new ODataConventionModelBuilder();
        odataBuilder.EntitySet<ProjectResponse>("projects");
        odataBuilder.EntitySet<ProjectCategoryResponse>("project-categories");
        odataBuilder.EntitySet<SquadProjectResponse>("squad-projects");
        
        odataBuilder.AddOdataConfigurations<ProjectResponseConfiguration, ProjectResponse>();
        odataBuilder.AddOdataConfigurations<ProjectCategoryResponseConfiguration, ProjectCategoryResponse>();
        odataBuilder.AddOdataConfigurations<SquadProjectResponseConfiguration, SquadProjectResponse>();
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