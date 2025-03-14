﻿// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Modules;

using System.Diagnostics.CodeAnalysis;
using Framework.ApiEssentials.Swagger;
using Framework.ModuleZ.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal class ApiDocumentationServiceModule : DependencyServiceModule
{
    [ExcludeFromCodeCoverage]
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.ConfigureSwagger(environment, configuration, title: "DistribuTe mutator api for teams",
            docPathPattern: "DistribuTe.Mutators.Teams.*.xml");
    }
}