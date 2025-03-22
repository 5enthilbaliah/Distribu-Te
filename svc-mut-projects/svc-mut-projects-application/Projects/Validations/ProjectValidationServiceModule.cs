namespace DistribuTe.Mutators.Projects.Application.Projects.Validations;

using DataContracts;
using ErrorOr;
using Framework.ModuleZ.Implementations;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class ProjectValidationServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddScoped<IPipelineBehavior<SpawnProjectCommand, ErrorOr<ProjectResponse>>,
            SpawnProjectCommandValidationBehavior>();
        services.AddScoped<IPipelineBehavior<CommitProjectCommand, ErrorOr<ProjectResponse>>,
            CommitProjectCommandValidationBehavior>();
        services.AddScoped<IPipelineBehavior<TrashProjectCommand, ErrorOr<bool>>,
            TrashProjectCommandValidationBehavior>();
    }
}