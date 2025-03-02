using DistribuTe.Mutators.Teams.Apis;
using DistribuTe.Mutators.Teams.Apis.Pipelines;
using DistribuTe.Mutators.Teams.Application;
using DistribuTe.Mutators.Teams.Domain;
using DistribuTe.Mutators.Teams.Host;
using DistribuTe.Mutators.Teams.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;
var configuration = builder.Configuration;

builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddDependencyModule<DomainServiceModule>(environment, configuration)
    .AddDependencyModule<ApplicationServiceModule>(environment, configuration)
    .AddDependencyModule<InfrastructureServiceModule>(environment, configuration)
    .AddDependencyModule<ApiServiceModule>(environment, configuration);

var app = builder.Build();
await app.Pipe<HttpsRedirectionPipeline>(environment, configuration)
    .Pipe<ApiDocumentationPipeline>(environment, configuration)
    .Pipe<CorsPipeline>(environment, configuration)
    .Pipe<AuthenticationPipeline>(environment, configuration)
    .Pipe<MvcPipeline>(environment, configuration)
    .RunAsync();