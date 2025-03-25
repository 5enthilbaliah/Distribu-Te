using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(assemblyName: "svc-mut-teams-unit-tests")]
namespace DistribuTe.Mutators.Teams.Infrastructure;

using Framework.AppEssentials;
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
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddMemoryCache();
        services.AddHealthChecks()
            .AddDbContextCheck<TeamSchemaDatabaseContext>(name: "sql_server", tags: ["db"]);
    }
}