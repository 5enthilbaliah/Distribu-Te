// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Modules;

using Application.Shared;
using Framework.ModuleZ.Implementations;
using Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal class IdentityServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddScoped<IRequestContext, RequestContext>();
        services.AddHttpContextAccessor();
    }
}