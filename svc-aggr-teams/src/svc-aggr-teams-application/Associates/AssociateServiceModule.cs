namespace DistribuTe.Aggregates.Teams.Application.Associates;

using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials.Linq;
using Framework.ModuleZ.Implementations;
using Mappers;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Validations;

public class AssociateServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        services.AddScoped<EntityLinqMapper<AssociateAggregate, AssociateId>, AssociateEntityLinqMapper>();
        
        services.AddScoped<IPipelineBehavior<YieldAssociatesQuery, ErrorOr<IList<AssociateModel>>>,
            YieldAssociatesQueryValidationBehavior>();
        services.AddScoped<IPipelineBehavior<PickAssociateQuery, ErrorOr<AssociateModel>>, 
            PickAssociateQueryValidationBehavior>();
    }
}