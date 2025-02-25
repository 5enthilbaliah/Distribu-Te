namespace DistribuTe.Framework.ModuleZ;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public interface IServiceModule
{
    void Register(IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration);
}