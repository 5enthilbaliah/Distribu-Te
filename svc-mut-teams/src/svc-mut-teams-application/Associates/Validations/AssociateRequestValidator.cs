namespace DistribuTe.Mutators.Teams.Application.Associates.Validations;

using DataContracts;
using FluentValidation;

public class AssociateRequestValidator : AbstractValidator<AssociateRequest>
{
    private static readonly char[] Genders = ['M', 'F', 'U'];
    
    public AssociateRequestValidator()
    {
        RuleFor(a => a.First_Name).NotEmpty();
        RuleFor(a => a.First_Name).MaximumLength(45);
        
        RuleFor(a => a.Last_Name).NotEmpty();
        RuleFor(a => a.Last_Name).MaximumLength(45);
        
        RuleFor(a => a.Middle_Name).MaximumLength(45);
        
        RuleFor(a => a.Email_Id).NotEmpty();
        RuleFor(a => a.Email_Id).MaximumLength(45);
        
        RuleFor(a => a.Gender).NotEmpty();
        RuleFor(a => a.Gender).Must(x => Genders.Contains(x))
            .WithMessage("Gender should be one of M, F, U.");
    }
}