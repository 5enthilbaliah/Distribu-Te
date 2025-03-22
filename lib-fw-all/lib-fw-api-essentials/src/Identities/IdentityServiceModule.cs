namespace DistribuTe.Framework.ApiEssentials.Identities;

using AppEssentials;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModuleZ.Implementations;

public class IdentityServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddScoped<IRequestContext, RequestContext>();
        services.AddHttpContextAccessor();
    }
}