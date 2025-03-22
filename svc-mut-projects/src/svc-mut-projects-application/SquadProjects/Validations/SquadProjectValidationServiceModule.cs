namespace DistribuTe.Mutators.Projects.Application.SquadProjects.Validations;

using DataContracts;
using ErrorOr;
using Framework.ModuleZ.Implementations;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class SquadProjectValidationServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddScoped<IPipelineBehavior<SpawnSquadProjectCommand, ErrorOr<SquadProjectResponse>>,
            SpawnSquadProjectCommandValidationBehavior>();
        services.AddScoped<IPipelineBehavior<CommitSquadProjectCommand, ErrorOr<SquadProjectResponse>>,
            CommitSquadProjectCommandValidationBehavior>();
        services.AddScoped<IPipelineBehavior<TrashSquadProjectCommand, ErrorOr<bool>>,
            TrashSquadProjectCommandValidationBehavior>();
    }
}