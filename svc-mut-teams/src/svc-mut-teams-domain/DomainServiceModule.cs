﻿namespace DistribuTe.Mutators.Teams.Domain;

using Framework.DomainEssentials.Settings;
using Framework.ModuleZ.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Settings;

public class DomainServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddOptions();
        services.Configure<CacheSettings>(configuration.GetSection(nameof(CacheSettings)));
        services.Configure<DistribuTeDbSettings>(configuration.GetSection(nameof(DistribuTeDbSettings)));
        services.Configure<TelemetrySettings>(configuration.GetSection(nameof(TelemetrySettings)));
    }
}