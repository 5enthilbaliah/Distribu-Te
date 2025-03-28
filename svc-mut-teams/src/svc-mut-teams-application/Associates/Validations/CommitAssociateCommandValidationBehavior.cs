﻿namespace DistribuTe.Mutators.Teams.Application.Associates.Validations;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using FluentValidation;
using Framework.AppEssentials;
using Framework.AppEssentials.Validations;
using MediatR;

public class CommitAssociateCommandValidationBehavior(IEntityReader<Associate, AssociateId> reader,
    IValidator<AssociateRequest> validator, IExistingEntityMarker<Associate, AssociateId> entityMarker) : 
    DistribuTeRequestValidationBehavior<AssociateRequest>(validator),
    IPipelineBehavior<CommitAssociateCommand, ErrorOr<AssociateResponse>>
{
    private readonly IEntityReader<Associate, AssociateId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly IExistingEntityMarker<Associate, AssociateId> _entityMarker = 
        entityMarker ?? throw new ArgumentNullException(nameof(entityMarker));


    public async Task<ErrorOr<AssociateResponse>> Handle(CommitAssociateCommand request,
        RequestHandlerDelegate<ErrorOr<AssociateResponse>> next, CancellationToken cancellationToken)
    {
        var (success, errors) = Validate(request.Associate);
        if (!success)
            return errors;
        
        var associateId = new AssociateId(request.Id);
        var existing = await _reader.PickAsync(associateId, cancellationToken);
        if (existing is null)
            return Errors.Associates.NotFound;
        
        var emailFound = await _reader.AnyAsync(a => a.Id != associateId &&
                a.EmailId == request.Associate.Email_Id, cancellationToken);
        if (emailFound)
            return Errors.Associates.DuplicateEmail;

        _entityMarker.Id = associateId;
        _entityMarker.Entity = existing;

        var response = await next();
        return response;
    }
}