namespace DistribuTe.Mutators.Projects.Application.Projects.Validations;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using FluentValidation;
using Framework.AppEssentials.Validations;
using MediatR;

public class SpawnProjectCommandValidationBehavior(IProjectsReader<Project, ProjectId> reader,
    IValidator<ProjectRequest> validator) : DistribuTeRequestValidationBehavior<ProjectRequest>(validator),
    IPipelineBehavior<SpawnProjectCommand, ErrorOr<ProjectResponse>>
{
    private readonly IProjectsReader<Project, ProjectId> _reader = 
        reader ?? throw new ArgumentNullException(nameof(reader));
    
    public async Task<ErrorOr<ProjectResponse>> Handle(SpawnProjectCommand request, 
        RequestHandlerDelegate<ErrorOr<ProjectResponse>> next, CancellationToken cancellationToken)
    {
        var (success, errors) = Validate(request.Project);
        if (!success)
            return errors;
        
        var codeFound = await _reader.AnyAsync(a => a.Code == request.Project.Code,
            cancellationToken);

        if (codeFound)
            return Errors.Projects.DuplicateCode;
        
        var response = await next();
        return response;
    }
}