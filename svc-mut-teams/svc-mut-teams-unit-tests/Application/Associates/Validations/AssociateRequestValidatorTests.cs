namespace DistribuTe.Mutators.Teams.UnitTests.Application.Associates.Validations;

using AutoFixture;
using FluentValidation.TestHelper;
using Teams.Application.Associates.DataContracts;
using Teams.Application.Associates.Validations;

public class AssociateRequestValidatorTests
{
    [Fact]
    public void AssociateRequestValidator_Validation_Fails()
    {
        // Arrange
        var longString = string.Join("", new Fixture().CreateMany<char>(100).ToArray());
        var request1 = new AssociateRequest
        {
            First_Name = string.Empty,
            Last_Name = string.Empty,
            Email_Id = string.Empty,
            Gender = 'K'
        };
        var request2 = new AssociateRequest
        {
            First_Name = longString,
            Last_Name = longString,
            Email_Id = longString,
            Middle_Name = longString,
        };
        
        // Act
        var sut = new AssociateRequestValidator();
        var result1 = sut!.TestValidate(request1);
        var result2 = sut!.TestValidate(request2);
        
        // Assert
        result1.ShouldHaveValidationErrorFor(x => x.First_Name);
        result1.ShouldHaveValidationErrorFor(x => x.Last_Name);
        result1.ShouldHaveValidationErrorFor(x => x.Email_Id);
        result1.ShouldHaveValidationErrorFor(x => x.Gender);
        
        result2.ShouldHaveValidationErrorFor(x => x.First_Name);
        result2.ShouldHaveValidationErrorFor(x => x.Last_Name);
        result2.ShouldHaveValidationErrorFor(x => x.Middle_Name);
        result2.ShouldHaveValidationErrorFor(x => x.Email_Id);
    }
}