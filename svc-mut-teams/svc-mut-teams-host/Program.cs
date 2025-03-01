using DistribuTe.Mutators.Teams.Apis;
using DistribuTe.Mutators.Teams.Apis.Settings;
using DistribuTe.Mutators.Teams.Application;
using DistribuTe.Mutators.Teams.Domain;
using DistribuTe.Mutators.Teams.Host;
using DistribuTe.Mutators.Teams.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;

builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true)
    .AddEnvironmentVariables();

var configuration = builder.Configuration;
var services = builder.Services;

services.AddDependencyModule<DomainServiceModule>(environment, configuration)
    .AddDependencyModule<ApplicationServiceModule>(environment, configuration)
    .AddDependencyModule<InfrastructureServiceModule>(environment, configuration)
    .AddDependencyModule<ApiServiceModule>(environment, configuration);

var app = builder.Build();

app.UseHttpsRedirection();

// Swagger
var swaggerSettings = configuration.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>()!;
app.UseSwagger(opt =>
{
    opt.RouteTemplate = swaggerSettings.JsonRoute;
    opt.PreSerializeFilters.Add((swaggerDoc, _) =>
    {
        swaggerDoc.Servers = 
        [
            new OpenApiServer
            {
                Url = swaggerSettings.ServerUrl
            }
        ];
    });
});
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint(swaggerSettings.UiEndpoint, swaggerSettings.Description);
});

// Api
app.UseCors("AllowAll");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();

app.Run();