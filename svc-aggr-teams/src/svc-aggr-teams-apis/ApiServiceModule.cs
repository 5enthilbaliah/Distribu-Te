namespace DistribuTe.Aggregates.Teams.Apis;

using Application.Associates.DataContracts;
using Application.SquadAssociates.DataContracts;
using Application.Squads.DataContracts;
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
        services.AddScoped<IOdataNavigator<AssociateModel>>(_ => 
            new OdataNavigator<AssociateModel>(WhereClauseItem.SpawnOne));
        services.AddScoped<IOdataNavigator<SquadModel>>(_ => 
            new OdataNavigator<SquadModel>(WhereClauseItem.SpawnOne));
        services.AddScoped<IOdataNavigator<SquadAssociateModel>>(_ => 
            new OdataNavigator<SquadAssociateModel>(WhereClauseItem.SpawnOne));

        services.AddKeyedScoped<IOdataPaginator, AssociateOdataPaginator>("associates");
        services.AddKeyedScoped<IOdataPaginator, SquadOdataPaginator>("squads");
        services.AddKeyedScoped<IOdataPaginator, SquadAssociateOdataPaginator>("squad-associates");
        
        services.Configure<ServiceSettings>(configuration.GetSection(nameof(ServiceSettings)));
        services.Configure<SwaggerSettings>(configuration.GetSection(nameof(SwaggerSettings)));
        services.Configure<AuthSettings>(configuration.GetSection(nameof(AuthSettings)));
        services.AddHealthChecks();
    }
}