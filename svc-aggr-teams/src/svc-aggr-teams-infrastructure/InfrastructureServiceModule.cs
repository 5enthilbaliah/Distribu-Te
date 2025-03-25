namespace DistribuTe.Aggregates.Teams.Infrastructure;

using Framework.ModuleZ.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

public class InfrastructureServiceModule : DependencyServiceModule
{
    public InfrastructureServiceModule()
    {
        PrependModule<PersistenceServiceModule>();
    }
    
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.AddHealthChecks()
            .AddDbContextCheck<TeamSchemaDatabaseContext>(name: "sql_server", tags: ["db"]);
    }
}