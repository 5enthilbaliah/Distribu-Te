namespace DistribuTe.Mutators.Teams.UnitTests.Application.Associates.Validations;

using System.Linq.Expressions;
using AutoFixture;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Framework.AppEssentials;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Teams.Application.Associates;
using Teams.Application.Associates.DataContracts;
using Teams.Application.Associates.Validations;
using Teams.Domain.Entities;

public class SpawnAssociateCommandValidationBehaviorTests
{
    private readonly ServiceProvider _serviceProvider;

    private readonly IEntityReader<Associate, AssociateId> _reader =
        Substitute.For<IEntityReader<Associate, AssociateId>>();
    private readonly IValidator<AssociateRequest> _validator = Substitute.For<IValidator<AssociateRequest>>();

    public SpawnAssociateCommandValidationBehaviorTests()
    {
        var services = new ServiceCollection();
        
        services.AddScoped(_ => _reader);
        services.AddScoped(_ => _validator);
        services.AddScoped<SpawnAssociateCommandValidationBehavior>();
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    [Fact]
    public void SpawnAssociateCommandValidationBehavior_NullArgument_ThrowsArgumentNullException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Action act = () => new SpawnAssociateCommandValidationBehavior(null, _validator);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'reader')");
        
        act = () => new SpawnAssociateCommandValidationBehavior(_reader, null);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'validator')");
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [Fact]
    public async Task Handle_WithValidationErrors_ReturnsValidationResult()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<SpawnAssociateCommand>();
        var response = fixture.Create<AssociateResponse>();
        _validator.Validate(Arg.Any<AssociateRequest>()).Returns(new ValidationResult
        {
            Errors = { new ValidationFailure("test", "test") }
        });
        
        // Act
        var sut = _serviceProvider.GetService<SpawnAssociateCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.FirstError.Code.Should().Be("test");
    }
    
    [Fact]
    public async Task Handle_DuplicateEmail_ReturnsValidationResult()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<SpawnAssociateCommand>();
        var response = fixture.Create<AssociateResponse>();
        _validator.Validate(Arg.Any<AssociateRequest>()).Returns(new ValidationResult());
        _reader.AnyAsync(Arg.Any<Expression<Func<Associate,bool>>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true));
            
        // Act
        var sut = _serviceProvider.GetService<SpawnAssociateCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.FirstError.Code.Should().Be("associate.duplicate_email");
    }
    
    [Fact]
    public async Task Handle_ValidRequest_ReturnsAssociateResponse()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<SpawnAssociateCommand>();
        var response = fixture.Create<AssociateResponse>();
        var entity = fixture.Create<Associate>();
        _validator.Validate(Arg.Any<AssociateRequest>()).Returns(new ValidationResult());
        _reader.AnyAsync(Arg.Any<Expression<Func<Associate,bool>>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(false));
            
        // Act
        var sut = _serviceProvider.GetService<SpawnAssociateCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(response), CancellationToken.None);
        
        // Assert
        result.Value.Should().BeEquivalentTo(response);
    }
}