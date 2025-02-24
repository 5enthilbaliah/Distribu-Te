namespace DistribuTe.Framework.WorkFlowZ.DependencyInjection;

using Implementations;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUsecase(this IServiceCollection services,
        Action<UsecaseOptionBuilder> usecaseBuild)
    {
        var ucOptbuilder = new UsecaseOptionBuilder();
        usecaseBuild(ucOptbuilder);
        
        ucOptbuilder.Build(services);
        return services;
    }

    public static IServiceCollection AddWorkFlowZ(this IServiceCollection services)
    {
        services.AddScoped<IUsecase, Usecase>();
        return services;
    }
}