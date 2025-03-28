using DistribuTe.Framework.ApiEssentials.Odata.ErrorHandling;
using DistribuTe.Framework.ModuleZ;
using DistribuTe.Mutators.Projects.Apis;
using DistribuTe.Mutators.Projects.Application;
using DistribuTe.Mutators.Projects.Domain;
using DistribuTe.Mutators.Projects.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;
var configuration = builder.Configuration;

// builder.Logging.ClearProviders();

builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddDependencyModule<DomainServiceModule>(environment, configuration)
    .AddDependencyModule<ApplicationServiceModule>(environment, configuration)
    .AddDependencyModule<InfrastructureServiceModule>(environment, configuration)
    .AddDependencyModule<ApiServiceModule>(environment, configuration)
    //TOMARE:: override the problem details factory here - order matters
    .AddDependencyModule<ErrorServiceModule>(environment, configuration);

var app = builder.Build();
await app.Setup(environment, configuration)
    .RunAsync();