namespace DistribuTe.Mutators.Projects.Infrastructure;

using Domain.Settings;
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
        
        var apiSettings = new DistribuTeApiSettings();
        configuration.GetSection(nameof(DistribuTeApiSettings)).Bind(apiSettings);
        services.AddHttpClient("teams-aggregate-api", httpclient =>
        {
            httpclient.BaseAddress = new Uri(apiSettings.TeamsAggregateApiBaseUrl);
        });
    }
}