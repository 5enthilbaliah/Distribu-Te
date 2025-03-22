namespace DistribuTe.Mutators.Teams.Application.Squads.Validations;

using DataContracts;
using ErrorOr;
using Framework.ModuleZ.Implementations;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class SquadValidationServiceModule: DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddScoped<IPipelineBehavior<SpawnSquadCommand, ErrorOr<SquadResponse>>,
            SpawnSquadCommandValidationBehavior>();
        services.AddScoped<IPipelineBehavior<CommitSquadCommand, ErrorOr<SquadResponse>>,
            CommitSquadCommandValidationBehavior>();
        services.AddScoped<IPipelineBehavior<TrashSquadCommand, ErrorOr<bool>>,
            TrashSquadCommandValidationBehavior>();
    }
}