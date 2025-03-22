namespace DistribuTe.Mutators.Teams.Application.SquadAssociates.Validations;

using DataContracts;
using ErrorOr;
using Framework.ModuleZ.Implementations;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class SquadAssociateValidationServiceModule: DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddScoped<IPipelineBehavior<SpawnSquadAssociateCommand, ErrorOr<SquadAssociateResponse>>,
            SpawnSquadAssociateCommandValidationBehavior>();
        services.AddScoped<IPipelineBehavior<CommitSquadAssociateCommand, ErrorOr<SquadAssociateResponse>>,
            CommitSquadAssociateCommandValidationBehavior>();
        services.AddScoped<IPipelineBehavior<TrashSquadAssociateCommand, ErrorOr<bool>>,
            TrashSquadAssociateCommandValidationBehavior>();
    }
}