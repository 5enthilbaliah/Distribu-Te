namespace DistribuTe.Framework.ModuleZ;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencyModule<TModule>(this IServiceCollection services,
        IWebHostEnvironment environment, IConfiguration configuration)
        where TModule : IServiceModule, new()
    {
        var module = new TModule();
        module.Register(services, environment, configuration);
        return services;
    }
}