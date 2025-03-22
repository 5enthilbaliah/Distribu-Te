namespace DistribuTe.Mutators.Teams.UnitTests.Application.SquadAssociates.Validations;

using System.Linq.Expressions;
using AutoFixture;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Teams.Application;
using Teams.Application.SquadAssociates;
using Teams.Application.SquadAssociates.DataContracts;
using Teams.Application.SquadAssociates.Validations;
using Teams.Domain.Entities;

public class SpawnSquadAssociateCommandValidationBehaviorTests
{
    private readonly ServiceProvider _serviceProvider;

    private readonly IEntityReader<SquadAssociate, SquadAssociateId> _reader =
        Substitute.For<IEntityReader<SquadAssociate, SquadAssociateId>>();
    private readonly IValidator<SquadAssociateRequest> _validator = Substitute.For<IValidator<SquadAssociateRequest>>();
    private readonly IEntityReader<Squad, SquadId> _squadReader =
        Substitute.For<IEntityReader<Squad, SquadId>>();
    private readonly IEntityReader<Associate, AssociateId> _associateReader =
        Substitute.For<IEntityReader<Associate, AssociateId>>();

    public SpawnSquadAssociateCommandValidationBehaviorTests()
    {
        var services = new ServiceCollection();
        
        services.AddScoped(_ => _reader);
        services.AddScoped(_ => _validator);
        services.AddScoped(_ => _squadReader);
        services.AddScoped(_ => _associateReader);
        services.AddScoped<SpawnSquadAssociateCommandValidationBehavior>();
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    [Fact]
    public void SpawnSquadAssociateCommandValidationBehavior_NullArgument_ThrowsArgumentNullException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Action act = () => new SpawnSquadAssociateCommandValidationBehavior(null, _validator,
            _squadReader, _associateReader);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'reader')");
        
        act = () => new SpawnSquadAssociateCommandValidationBehavior(_reader, null, 
            _squadReader, _associateReader);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'validator')");
        
        act = () => new SpawnSquadAssociateCommandValidationBehavior(_reader, _validator, 
            null, _associateReader);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'squadReader')");
        
        act = () => new SpawnSquadAssociateCommandValidationBehavior(_reader, _validator, 
            _squadReader, null);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'associateReader')");
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [Fact]
    public async Task Handle_WithValidationErrors_ReturnsValidationResult()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<SpawnSquadAssociateCommand>();
        var response = fixture.Create<SquadAssociateResponse>();
        _validator.Validate(Arg.Any<SquadAssociateRequest>()).Returns(new ValidationResult
        {
            Errors = { new ValidationFailure("test", "test") }
        });
        
        // Act
        var sut = _serviceProvider.GetService<SpawnSquadAssociateCommandValidationBehavior>();
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
        var command = fixture.Create<SpawnSquadAssociateCommand>();
        var response = fixture.Create<SquadAssociateResponse>();
        _validator.Validate(Arg.Any<SquadAssociateRequest>()).Returns(new ValidationResult());
        
        _squadReader.AnyAsync(Arg.Any<Expression<Func<Squad,bool>>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(false));
            
        // Act
        var sut = _serviceProvider.GetService<SpawnSquadAssociateCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.FirstError.Code.Should().Be("squad.not_found");
    }
    
    [Fact]
    public async Task Handle_InvalidAssociateId_ReturnsValidationResult()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<SpawnSquadAssociateCommand>();
        var response = fixture.Create<SquadAssociateResponse>();
        _validator.Validate(Arg.Any<SquadAssociateRequest>()).Returns(new ValidationResult());
        
        _squadReader.AnyAsync(Arg.Any<Expression<Func<Squad,bool>>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true));
        _associateReader.AnyAsync(Arg.Any<Expression<Func<Associate,bool>>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(false));
            
        // Act
        var sut = _serviceProvider.GetService<SpawnSquadAssociateCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.FirstError.Code.Should().Be("associate.not_found");
    }
    
    [Fact]
    public async Task Handle_DuplicateAllocation_ReturnsValidationResult()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<SpawnSquadAssociateCommand>();
        var response = fixture.Create<SquadAssociateResponse>();
        _validator.Validate(Arg.Any<SquadAssociateRequest>()).Returns(new ValidationResult());
        
        _squadReader.AnyAsync(Arg.Any<Expression<Func<Squad,bool>>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true));
        _associateReader.AnyAsync(Arg.Any<Expression<Func<Associate,bool>>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true));
        _reader.AnyAsync(Arg.Any<Expression<Func<SquadAssociate,bool>>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true));
        
        // Act
        var sut = _serviceProvider.GetService<SpawnSquadAssociateCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.FirstError.Code.Should().Be("squad_associate.duplicate_allocation");
    }
    
    [Fact]
    public async Task Handle_ValidRequest_ReturnsSquadAssociateResponse()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<SpawnSquadAssociateCommand>();
        var response = fixture.Create<SquadAssociateResponse>();
        _validator.Validate(Arg.Any<SquadAssociateRequest>()).Returns(new ValidationResult());
        
        _squadReader.AnyAsync(Arg.Any<Expression<Func<Squad,bool>>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true));
        _associateReader.AnyAsync(Arg.Any<Expression<Func<Associate,bool>>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true));
        _reader.AnyAsync(Arg.Any<Expression<Func<SquadAssociate,bool>>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(false));
            
        // Act
        var sut = _serviceProvider.GetService<SpawnSquadAssociateCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.Value.Should().BeEquivalentTo(response);
    }
}