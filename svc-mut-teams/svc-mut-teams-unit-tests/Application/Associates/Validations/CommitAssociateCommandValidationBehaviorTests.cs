namespace DistribuTe.Mutators.Teams.UnitTests.Application.Associates.Validations;

using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Teams.Application;
using Teams.Application.Associates;
using Teams.Application.Associates.DataContracts;
using Teams.Application.Associates.Validations;
using Teams.Application.Shared;

public class CommitAssociateCommandValidationBehaviorTests
{
    private readonly ServiceProvider _serviceProvider;

    private readonly ITeamsReader<Associate, AssociateId> _reader =
        Substitute.For<ITeamsReader<Associate, AssociateId>>();
    private readonly IExistingEntityMarker<Associate, AssociateId> _entityMarker =
        Substitute.For<IExistingEntityMarker<Associate, AssociateId>>();
    private readonly IValidator<AssociateRequest> _validator = Substitute.For<IValidator<AssociateRequest>>();

    public CommitAssociateCommandValidationBehaviorTests()
    {
        var services = new ServiceCollection();
        
        services.AddScoped(_ => _reader);
        services.AddScoped(_ => _entityMarker);
        services.AddScoped(_ => _validator);
        services.AddScoped<CommitAssociateCommandValidationBehavior>();
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    [Fact]
    public void CommitAssociateCommandValidationBehavior_NullArgument_ThrowsArgumentNullException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Action act = () => new CommitAssociateCommandValidationBehavior(null, _validator, _entityMarker);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'reader')");
        
        act = () => new CommitAssociateCommandValidationBehavior(_reader, null, _entityMarker);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'validator')");
        
        act = () => new CommitAssociateCommandValidationBehavior(_reader, _validator, null);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'entityMarker')");
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [Fact]
    public async Task Handle_WithValidationErrors_ReturnsValidationResult()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<CommitAssociateCommand>();
        var response = fixture.Create<AssociateResponse>();
        _validator.Validate(Arg.Any<AssociateRequest>()).Returns(new ValidationResult
        {
            Errors = { new ValidationFailure("test", "test") }
        });
        
        // Act
        var sut = _serviceProvider.GetService<CommitAssociateCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.FirstError.Code.Should().Be("test");
    }
    
    [Fact]
    public async Task Handle_InvalidAssociateId_ReturnsValidationResult()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<CommitAssociateCommand>();
        var response = fixture.Create<AssociateResponse>();
        _validator.Validate(Arg.Any<AssociateRequest>()).Returns(new ValidationResult());
        _reader.PickAsync(Arg.Any<AssociateId>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(default(Associate)));
            
        // Act
        var sut = _serviceProvider.GetService<CommitAssociateCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.FirstError.Code.Should().Be("associate.not_found");
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsAssociateResponse()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<CommitAssociateCommand>();
        var response = fixture.Create<AssociateResponse>();
        var entity = fixture.Create<Associate>();
        _validator.Validate(Arg.Any<AssociateRequest>()).Returns(new ValidationResult());
        _reader.PickAsync(Arg.Any<AssociateId>(), Arg.Any<CancellationToken>())!
            .Returns(Task.FromResult(entity));
            
        // Act
        var sut = _serviceProvider.GetService<CommitAssociateCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.Value.Should().BeEquivalentTo(response);
    }
}