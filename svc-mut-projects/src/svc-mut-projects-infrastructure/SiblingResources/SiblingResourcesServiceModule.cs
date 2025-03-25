namespace DistribuTe.Mutators.Projects.Infrastructure.SiblingResources;

using Application;
using Domain.Settings;
using Framework.ModuleZ.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

public class SiblingResourcesServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        var apiSettings = new DistribuTeApiSettings();
        configuration.GetSection(nameof(DistribuTeApiSettings)).Bind(apiSettings);

        services.AddRefitClient<ITeamsAggregateApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiSettings.TeamsAggregateApiBaseUrl));
        services.AddScoped<ITeamsAggregateApiReader, TeamsAggregateApiReader>();
    }
}