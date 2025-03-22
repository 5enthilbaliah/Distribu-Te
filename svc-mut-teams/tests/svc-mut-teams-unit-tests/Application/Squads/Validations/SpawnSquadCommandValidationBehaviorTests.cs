namespace DistribuTe.Mutators.Teams.UnitTests.Application.Squads.Validations;

using System.Linq.Expressions;
using AutoFixture;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Framework.AppEssentials;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Teams.Application.Squads;
using Teams.Application.Squads.DataContracts;
using Teams.Application.Squads.Validations;
using Teams.Domain.Entities;
using LambdaCompare = Framework.TestEssentials.LambdaCompare.Lambda;

public class SpawnSquadCommandValidationBehaviorTests
{
    private readonly ServiceProvider _serviceProvider;

    private readonly IEntityReader<Squad, SquadId> _reader =
        Substitute.For<IEntityReader<Squad, SquadId>>();
    private readonly IValidator<SquadRequest> _validator = Substitute.For<IValidator<SquadRequest>>();

    public SpawnSquadCommandValidationBehaviorTests()
    {
        var services = new ServiceCollection();
        
        services.AddScoped(_ => _reader);
        services.AddScoped(_ => _validator);
        services.AddScoped<SpawnSquadCommandValidationBehavior>();
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    [Fact]
    public void SpawnSquadCommandValidationBehavior_NullArgument_ThrowsArgumentNullException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Action act = () => new SpawnSquadCommandValidationBehavior(null, _validator);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'reader')");
        
        act = () => new SpawnSquadCommandValidationBehavior(_reader, null);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'validator')");
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [Fact]
    public async Task Handle_WithValidationErrors_ReturnsValidationResult()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<SpawnSquadCommand>();
        var response = fixture.Create<SquadResponse>();
        _validator.Validate(Arg.Any<SquadRequest>()).Returns(new ValidationResult
        {
            Errors = { new ValidationFailure("test", "test") }
        });
        
        // Act
        var sut = _serviceProvider.GetService<SpawnSquadCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.FirstError.Code.Should().Be("test");
    }
    
    [Fact]
    public async Task Handle_DuplicateCode_ReturnsValidationResult()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<SpawnSquadCommand>();
        var response = fixture.Create<SquadResponse>();
        _validator.Validate(Arg.Any<SquadRequest>()).Returns(new ValidationResult());
        _reader.AnyAsync(Arg.Any<Expression<Func<Squad,bool>>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true));
            
        // Act
        var sut = _serviceProvider.GetService<SpawnSquadCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.FirstError.Code.Should().Be("squad.duplicate_code");
    }
    
    [Fact]
    public async Task Handle_DuplicateName_ReturnsValidationResult()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<SpawnSquadCommand>();
        var response = fixture.Create<SquadResponse>();
        _validator.Validate(Arg.Any<SquadRequest>()).Returns(new ValidationResult());
        
        _reader.AnyAsync(Arg.Is<Expression<Func<Squad, bool>>>(expr => 
                    LambdaCompare.Eq(expr, a => a.Code == command.Squad.Code)), 
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(false));
        
        _reader.AnyAsync(Arg.Is<Expression<Func<Squad, bool>>>(expr => 
                    LambdaCompare.Eq(expr, a => a.Name == command.Squad.Name)), 
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true));
            
        // Act
        var sut = _serviceProvider.GetService<SpawnSquadCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.FirstError.Code.Should().Be("squad.duplicate_name");
    }
    
    [Fact]
    public async Task Handle_ValidRequest_ReturnsSquadResponse()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<SpawnSquadCommand>();
        var response = fixture.Create<SquadResponse>();
        _validator.Validate(Arg.Any<SquadRequest>()).Returns(new ValidationResult());
        _reader.AnyAsync(Arg.Any<Expression<Func<Squad,bool>>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(false));
            
        // Act
        var sut = _serviceProvider.GetService<SpawnSquadCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.Value.Should().BeEquivalentTo(response);
    }
}