namespace DistribuTe.Mutators.Teams.UnitTests.Application.Associates.Validations;

using AutoFixture;
using FluentAssertions;
using Framework.AppEssentials;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Teams.Application;
using Teams.Application.Associates;
using Teams.Application.Associates.Validations;
using Teams.Domain.Entities;

public class TrashAssociateCommandValidationBehaviorTests
{
    private readonly ServiceProvider _serviceProvider;

    private readonly ITeamsReader<Associate, AssociateId> _reader =
        Substitute.For<ITeamsReader<Associate, AssociateId>>();
    private readonly IExistingEntityMarker<Associate, AssociateId> _entityMarker =
        Substitute.For<IExistingEntityMarker<Associate, AssociateId>>();
    
    public TrashAssociateCommandValidationBehaviorTests()
    {
        var services = new ServiceCollection();
        
        services.AddScoped(_ => _reader);
        services.AddScoped(_ => _entityMarker);
        services.AddScoped<TrashAssociateCommandValidationBehavior>();
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    [Fact]
    public void TrashAssociateCommandValidationBehavior_NullArgument_ThrowsArgumentNullException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Action act = () => new TrashAssociateCommandValidationBehavior(null, _entityMarker);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'reader')");
        
        act = () => new TrashAssociateCommandValidationBehavior(_reader, null);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'entityMarker')");

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
    
    [Fact]
    public async Task Handle_InvalidAssociateId_ReturnsValidationResult()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<TrashAssociateCommand>();
        _reader.PickAsync(Arg.Any<AssociateId>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(default(Associate)));
            
        // Act
        var sut = _serviceProvider.GetService<TrashAssociateCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(true), CancellationToken.None);
        
        // Assert
        result.FirstError.Code.Should().Be("associate.not_found");
    }
    
    [Fact]
    public async Task Handle_ValidRequest_ReturnsTrue()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<TrashAssociateCommand>();
        var entity = fixture.Create<Associate>();
        _reader.PickAsync(Arg.Any<AssociateId>(), Arg.Any<CancellationToken>())!
            .Returns(Task.FromResult(entity));
            
        // Act
        var sut = _serviceProvider.GetService<TrashAssociateCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(true), CancellationToken.None);
        
        // Assert
        result.Value.Should().BeTrue();
    }
}