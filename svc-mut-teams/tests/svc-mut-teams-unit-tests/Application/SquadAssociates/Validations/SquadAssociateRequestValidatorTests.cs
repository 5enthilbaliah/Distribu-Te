namespace DistribuTe.Mutators.Teams.UnitTests.Application.SquadAssociates.Validations;

using AutoFixture;
using FluentValidation.TestHelper;
using Teams.Application.SquadAssociates.DataContracts;
using Teams.Application.SquadAssociates.Validations;

public class SquadAssociateRequestValidatorTests
{
    [Fact]
    public void SquadAssociateRequestValidator_Validation_Fails()
    {
        // Arrange
        var fixture = new Fixture();
        var request1 = new SquadAssociateRequest
        {
            Squad_Id = 0,
            Associate_Id = 0,
            Capacity = -1
        };
        var request2 = new SquadAssociateRequest
        {
            Squad_Id = 10,
            Associate_Id = 11,
            Capacity = 10.0m,
            Started_On = DateTime.Today,
            Ended_On = DateTime.Today.AddDays(-1),
        };
        
        // Act
        var sut = new SquadAssociateRequestValidator();
        var result1 = sut!.TestValidate(request1);
        var result2 = sut!.TestValidate(request2);
        
        // Assert
        result1.ShouldHaveValidationErrorFor(x => x.Squad_Id);
        result1.ShouldHaveValidationErrorFor(x => x.Associate_Id);
        result1.ShouldHaveValidationErrorFor(x => x.Capacity);
        
        result2.ShouldHaveValidationErrorFor(x => x.Capacity);
        result2.ShouldHaveValidationErrorFor(x => x.Ended_On);
    }
}