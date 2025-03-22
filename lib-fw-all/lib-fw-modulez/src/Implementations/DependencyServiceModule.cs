namespace DistribuTe.Framework.ModuleZ.Implementations;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public abstract class DependencyServiceModule : IServiceModule
{
    private readonly List<IServiceModule> _before = new();
    private readonly List<IServiceModule> _after = new();

    protected void PrependModule<TModule>()
        where TModule : IServiceModule, new()
    {
        var module = new TModule();
        _before.Add(module);
    }
    
    protected void AppendModule<TModule>()
        where TModule : IServiceModule, new()
    {
        var module = new TModule();
        _after.Add(module);
    }

    protected abstract void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment,
        IConfiguration configuration); 

    public void Register(IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        foreach (var module in _before)
        {
            module.Register(services, environment, configuration);
        }
        
        RegisterCurrent(services, environment, configuration);
        
        foreach (var module in _after)
        {
            module.Register(services, environment, configuration);
        }
    }
}