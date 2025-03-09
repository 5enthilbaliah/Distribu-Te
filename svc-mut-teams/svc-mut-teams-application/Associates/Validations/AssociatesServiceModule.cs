namespace DistribuTe.Mutators.Teams.Application.Associates.Validations;

using DataContracts;
using ErrorOr;
using Framework.ModuleZ.Implementations;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class AssociateValidationServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddScoped<IPipelineBehavior<SpawnAssociateCommand, ErrorOr<AssociateResponse>>,
            SpawnAssociateCommandValidationBehavior>();
        services.AddScoped<IPipelineBehavior<CommitAssociateCommand, ErrorOr<AssociateResponse>>,
            CommitAssociateCommandValidationBehavior>();
        services.AddScoped<IPipelineBehavior<TrashAssociateCommand, ErrorOr<bool>>,
            TrashAssociateCommandValidationBehavior>();
    }
}