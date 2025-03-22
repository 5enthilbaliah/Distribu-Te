namespace DistribuTe.Mutators.Teams.UnitTests.Application.Squads.Validations;

using AutoFixture;
using FluentValidation.TestHelper;
using Framework.TestEssentials.AutoFixture;
using Teams.Application.Squads.DataContracts;
using Teams.Application.Squads.Validations;

public class SquadRequestValidatorTests
{
    [Fact]
    public void SquadRequestValidator_Validation_Fails()
    {
        // Arrange
        var fixture = new Fixture();
        var longString = fixture.GenerateLengthLimitedString(100);
        var request1 = new SquadRequest
        {
            Name = string.Empty,
            Code = string.Empty
        };
        var request2 = new SquadRequest
        {
            Name = longString,
            Code = longString,
            Description = fixture.GenerateLengthLimitedString(250)
        };
        
        // Act
        var sut = new SquadRequestValidator();
        var result1 = sut!.TestValidate(request1);
        var result2 = sut!.TestValidate(request2);
        
        // Assert
        result1.ShouldHaveValidationErrorFor(x => x.Name);
        result1.ShouldHaveValidationErrorFor(x => x.Code);
        
        result2.ShouldHaveValidationErrorFor(x => x.Name);
        result2.ShouldHaveValidationErrorFor(x => x.Code);
        result2.ShouldHaveValidationErrorFor(x => x.Description);
    }
}