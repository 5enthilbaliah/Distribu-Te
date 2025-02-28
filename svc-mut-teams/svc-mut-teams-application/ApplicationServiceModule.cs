namespace DistribuTe.Mutators.Teams.Application;

using Framework.ModuleZ.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;

public class ApplicationServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<IRequestContext>());
        
        // Register global behaviors - else register with application scope folder
        // services.AddTransient(typeof(IPipelineBehavior<,>, typeof(AuthenticationBehavior<,>)));
    }
}