namespace DistribuTe.Mutators.Teams.UnitTests.Application.Squads.Validations;

using AutoFixture;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Framework.AppEssentials;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Teams.Application;
using Teams.Application.Squads;
using Teams.Application.Squads.DataContracts;
using Teams.Application.Squads.Validations;
using Teams.Domain.Entities;

public class CommitSquadCommandValidationBehaviorTests
{
    private readonly ServiceProvider _serviceProvider;

    private readonly IEntityReader<Squad, SquadId> _reader =
        Substitute.For<IEntityReader<Squad, SquadId>>();
    private readonly IExistingEntityMarker<Squad, SquadId> _entityMarker =
        Substitute.For<IExistingEntityMarker<Squad, SquadId>>();
    private readonly IValidator<SquadRequest> _validator = Substitute.For<IValidator<SquadRequest>>();

    public CommitSquadCommandValidationBehaviorTests()
    {
        var services = new ServiceCollection();
        
        services.AddScoped(_ => _reader);
        services.AddScoped(_ => _entityMarker);
        services.AddScoped(_ => _validator);
        services.AddScoped<CommitSquadCommandValidationBehavior>();
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    [Fact]
    public void CommitSquadCommandValidationBehavior_NullArgument_ThrowsArgumentNullException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Action act = () => new CommitSquadCommandValidationBehavior(null, _validator, _entityMarker);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'reader')");
        
        act = () => new CommitSquadCommandValidationBehavior(_reader, null, _entityMarker);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'validator')");
        
        act = () => new CommitSquadCommandValidationBehavior(_reader, _validator, null);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'entityMarker')");
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [Fact]
    public async Task Handle_WithValidationErrors_ReturnsValidationResult()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<CommitSquadCommand>();
        var response = fixture.Create<SquadResponse>();
        _validator.Validate(Arg.Any<SquadRequest>()).Returns(new ValidationResult
        {
            Errors = { new ValidationFailure("test", "test") }
        });
        
        // Act
        var sut = _serviceProvider.GetService<CommitSquadCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.FirstError.Code.Should().Be("test");
    }
    
    [Fact]
    public async Task Handle_InvalidSquadId_ReturnsValidationResult()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<CommitSquadCommand>();
        var response = fixture.Create<SquadResponse>();
        _validator.Validate(Arg.Any<SquadRequest>()).Returns(new ValidationResult());
        _reader.PickAsync(Arg.Any<SquadId>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(default(Squad)));
            
        // Act
        var sut = _serviceProvider.GetService<CommitSquadCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.FirstError.Code.Should().Be("squad.not_found");
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsSquadResponse()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<CommitSquadCommand>();
        var response = fixture.Create<SquadResponse>();
        var entity = fixture.Create<Squad>();
        _validator.Validate(Arg.Any<SquadRequest>()).Returns(new ValidationResult());
        _reader.PickAsync(Arg.Any<SquadId>(), Arg.Any<CancellationToken>())!
            .Returns(Task.FromResult(entity));
            
        // Act
        var sut = _serviceProvider.GetService<CommitSquadCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.Value.Should().BeEquivalentTo(response);
    }
}