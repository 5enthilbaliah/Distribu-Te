using DistribuTe.Aggregates.Teams.Domain;
using DistribuTe.Aggregates.Teams.Infrastructure;
using DistribuTe.Framework.ModuleZ;
using DistribuTe.Framework.OData.ErrorHandling;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;
var configuration = builder.Configuration;

builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true)
    .AddEnvironmentVariables();
    
builder.Services.AddDependencyModule<DomainServiceModule>(environment, configuration)
    // .AddDependencyModule<ApplicationServiceModule>(environment, configuration)
    .AddDependencyModule<InfrastructureServiceModule>(environment, configuration)
    // .AddDependencyModule<ApiServiceModule>(environment, configuration)
    //TOMARE:: override the problem details factory here - order matters
    .AddDependencyModule<ErrorServiceModule>(environment, configuration);
    
var app = builder.Build();