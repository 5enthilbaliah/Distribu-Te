namespace DistribuTe.Framework.ModuleZ.Implementations;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public abstract class DependencyServiceModule : IServiceModule
{
    private readonly List<IServiceModule> _modules = new();

    protected void AddModule<TModule>()
        where TModule : IServiceModule, new()
    {
        var module = new TModule();
        _modules.Add(module);
    }

    protected abstract void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment,
        IConfiguration configuration); 

    public void Register(IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        foreach (var module in _modules)
        {
            module.Register(services, environment, configuration);
        }
        
        RegisterCurrent(services, environment, configuration);
    }
}