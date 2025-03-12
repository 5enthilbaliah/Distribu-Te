namespace DistribuTe.Mutators.Teams.UnitTests.Application.SquadAssociates.Validations;

using AutoFixture;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Teams.Application;
using Teams.Application.Shared;
using Teams.Application.SquadAssociates;
using Teams.Application.SquadAssociates.DataContracts;
using Teams.Application.SquadAssociates.Validations;
using Teams.Domain.Entities;

public class CommitSquadAssociateCommandValidationBehaviorTests
{
    private readonly ServiceProvider _serviceProvider;

    private readonly ITeamsReader<SquadAssociate, SquadAssociateId> _reader =
        Substitute.For<ITeamsReader<SquadAssociate, SquadAssociateId>>();
    private readonly IExistingEntityMarker<SquadAssociate, SquadAssociateId> _entityMarker =
        Substitute.For<IExistingEntityMarker<SquadAssociate, SquadAssociateId>>();
    private readonly IValidator<SquadAssociateRequest> _validator = Substitute.For<IValidator<SquadAssociateRequest>>();

    public CommitSquadAssociateCommandValidationBehaviorTests()
    {
        var services = new ServiceCollection();
        
        services.AddScoped(_ => _reader);
        services.AddScoped(_ => _entityMarker);
        services.AddScoped(_ => _validator);
        services.AddScoped<CommitSquadAssociateCommandValidationBehavior>();
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    [Fact]
    public void CommitSquadAssociateCommandValidationBehavior_NullArgument_ThrowsArgumentNullException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Action act = () => new CommitSquadAssociateCommandValidationBehavior(null, _validator, _entityMarker);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'reader')");
        
        act = () => new CommitSquadAssociateCommandValidationBehavior(_reader, null, _entityMarker);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'validator')");
        
        act = () => new CommitSquadAssociateCommandValidationBehavior(_reader, _validator, null);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'entityMarker')");
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [Fact]
    public async Task Handle_WithValidationErrors_ReturnsValidationResult()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<CommitSquadAssociateCommand>();
        var response = fixture.Create<SquadAssociateResponse>();
        _validator.Validate(Arg.Any<SquadAssociateRequest>()).Returns(new ValidationResult
        {
            Errors = { new ValidationFailure("test", "test") }
        });
        
        // Act
        var sut = _serviceProvider.GetService<CommitSquadAssociateCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.FirstError.Code.Should().Be("test");
    }
    
    [Fact]
    public async Task Handle_InvalidSquadAssociateId_ReturnsValidationResult()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<CommitSquadAssociateCommand>();
        var response = fixture.Create<SquadAssociateResponse>();
        _validator.Validate(Arg.Any<SquadAssociateRequest>()).Returns(new ValidationResult());
        _reader.PickAsync(Arg.Any<SquadAssociateId>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(default(SquadAssociate)));
            
        // Act
        var sut = _serviceProvider.GetService<CommitSquadAssociateCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.FirstError.Code.Should().Be("squad_associate.not_found");
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsSquadAssociateResponse()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<CommitSquadAssociateCommand>();
        var response = fixture.Create<SquadAssociateResponse>();
        var entity = fixture.Create<SquadAssociate>();
        _validator.Validate(Arg.Any<SquadAssociateRequest>()).Returns(new ValidationResult());
        _reader.PickAsync(Arg.Any<SquadAssociateId>(), Arg.Any<CancellationToken>())!
            .Returns(Task.FromResult(entity));
            
        // Act
        var sut = _serviceProvider.GetService<CommitSquadAssociateCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.Value.Should().BeEquivalentTo(response);
    }
}