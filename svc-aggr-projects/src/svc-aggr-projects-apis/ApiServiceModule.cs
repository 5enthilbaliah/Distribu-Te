namespace DistribuTe.Aggregates.Projects.Apis;

using Application.ProjectCategories.DataContracts;
using Application.Projects.DataContracts;
using Application.SquadProjects.DataContracts;
using Framework.ApiEssentials;
using Framework.ApiEssentials.Auth;
using Framework.ApiEssentials.Cors;
using Framework.ApiEssentials.Identities;
using Framework.ApiEssentials.Odata;
using Framework.ApiEssentials.Odata.Implementations;
using Framework.ApiEssentials.Swagger;
using Framework.ApiEssentials.Versioning;
using Framework.AppEssentials.Linq;
using Framework.ModuleZ.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Odata;

public class ApiServiceModule : DependencyServiceModule
{
    public ApiServiceModule()
    {
        PrependModule<IdentityServiceModule>();
        PrependModule<AuthenticationServiceModule>();
        PrependModule<ApiVersionServiceModule>();
        PrependModule<CorsServiceModule>();
        PrependModule<ControllerServiceModule>();
        PrependModule<ApiDocumentationServiceModule>();
    }
    
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddTransient(_ => new OdataFilterVisitor(WhereClauseItem.SpawnOne));
        services.AddScoped<IOdataNavigator<ProjectModel>>(_ => 
            new OdataNavigator<ProjectModel>(WhereClauseItem.SpawnOne));
        services.AddScoped<IOdataNavigator<ProjectCategoryModel>>(_ => 
            new OdataNavigator<ProjectCategoryModel>(WhereClauseItem.SpawnOne));
        services.AddScoped<IOdataNavigator<SquadProjectModel>>(_ => 
            new OdataNavigator<SquadProjectModel>(WhereClauseItem.SpawnOne));

        services.AddKeyedScoped<IOdataPaginator, ProjectOdataPaginator>("projects");
        services.AddKeyedScoped<IOdataPaginator, ProjectCategoryOdataPaginator>("project-categories");
        services.AddKeyedScoped<IOdataPaginator, SquadProjectOdataPaginator>("squad-projects");
        
        services.Configure<ServiceSettings>(configuration.GetSection(nameof(ServiceSettings)));
        services.Configure<SwaggerSettings>(configuration.GetSection(nameof(SwaggerSettings)));
        services.Configure<AuthSettings>(configuration.GetSection(nameof(AuthSettings)));
    }
}