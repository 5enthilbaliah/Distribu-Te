namespace DistribuTe.Framework.OData.ErrorHandling;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModuleZ.Implementations;

public class ErrorServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddSingleton<ProblemDetailsFactory, DistribuTeProblemDetailsFactory>();
    }
}