namespace DistribuTe.Mutators.Teams.UnitTests.Application.Squads.Validations;

using AutoFixture;
using FluentAssertions;
using Framework.AppEssentials;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Teams.Application;
using Teams.Application.Squads;
using Teams.Application.Squads.Validations;
using Teams.Domain.Entities;

public class TrashSquadCommandValidationBehaviorTests
{
    private readonly ServiceProvider _serviceProvider;

    private readonly IEntityReader<Squad, SquadId> _reader =
        Substitute.For<IEntityReader<Squad, SquadId>>();
    private readonly IExistingEntityMarker<Squad, SquadId> _entityMarker =
        Substitute.For<IExistingEntityMarker<Squad, SquadId>>();
    
    public TrashSquadCommandValidationBehaviorTests()
    {
        var services = new ServiceCollection();
        
        services.AddScoped(_ => _reader);
        services.AddScoped(_ => _entityMarker);
        services.AddScoped<TrashSquadCommandValidationBehavior>();
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    [Fact]
    public void TrashSquadCommandValidationBehavior_NullArgument_ThrowsArgumentNullException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Action act = () => new TrashSquadCommandValidationBehavior(null, _entityMarker);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'reader')");
        
        act = () => new TrashSquadCommandValidationBehavior(_reader, null);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'entityMarker')");

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
    
    [Fact]
    public async Task Handle_InvalidSquadId_ReturnsValidationResult()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<TrashSquadCommand>();
        _reader.PickAsync(Arg.Any<SquadId>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(default(Squad)));
            
        // Act
        var sut = _serviceProvider.GetService<TrashSquadCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(true), CancellationToken.None);
        
        // Assert
        result.FirstError.Code.Should().Be("squad.not_found");
    }
    
    [Fact]
    public async Task Handle_ValidRequest_ReturnsTrue()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<TrashSquadCommand>();
        var entity = fixture.Create<Squad>();
        _reader.PickAsync(Arg.Any<SquadId>(), Arg.Any<CancellationToken>())!
            .Returns(Task.FromResult(entity));
            
        // Act
        var sut = _serviceProvider.GetService<TrashSquadCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(true), CancellationToken.None);
        
        // Assert
        result.Value.Should().BeTrue();
    }
}