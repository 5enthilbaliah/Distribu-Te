namespace DistribuTe.Mutators.Teams.Host;

using Framework.ModuleZ;

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

    public static WebApplication Pipe<TPipeline>(this WebApplication app,
        IWebHostEnvironment environment, IConfiguration configuration)
        where TPipeline : IMiddlewarePipeline, new()
    {
        var pipeline = new TPipeline();
        pipeline.Setup(app, environment, configuration);
        return app;
    }
}