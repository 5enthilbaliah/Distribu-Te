namespace DistribuTe.Mutators.Teams.Infrastructure;

using Framework.ModuleZ.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

public class InfrastructureServiceModule : DependencyServiceModule
{
    public InfrastructureServiceModule()
    {
        AddModule<PersistenceServiceModule>();
    }
    
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddMemoryCache();
    }
}