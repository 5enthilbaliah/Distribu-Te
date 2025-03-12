namespace DistribuTe.Mutators.Teams.UnitTests.Application.SquadAssociates.Validations;

using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Teams.Application;
using Teams.Application.Shared;
using Teams.Application.SquadAssociates;
using Teams.Application.SquadAssociates.Validations;
using Teams.Domain.Entities;

public class TrashSquadAssociateCommandValidationBehaviorTests
{
    private readonly ServiceProvider _serviceProvider;

    private readonly ITeamsReader<SquadAssociate, SquadAssociateId> _reader =
        Substitute.For<ITeamsReader<SquadAssociate, SquadAssociateId>>();
    private readonly IExistingEntityMarker<SquadAssociate, SquadAssociateId> _entityMarker =
        Substitute.For<IExistingEntityMarker<SquadAssociate, SquadAssociateId>>();
    
    public TrashSquadAssociateCommandValidationBehaviorTests()
    {
        var services = new ServiceCollection();
        
        services.AddScoped(_ => _reader);
        services.AddScoped(_ => _entityMarker);
        services.AddScoped<TrashSquadAssociateCommandValidationBehavior>();
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    [Fact]
    public void TrashSquadAssociateCommandValidationBehavior_NullArgument_ThrowsArgumentNullException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Action act = () => new TrashSquadAssociateCommandValidationBehavior(null, _entityMarker);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'reader')");
        
        act = () => new TrashSquadAssociateCommandValidationBehavior(_reader, null);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'entityMarker')");

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
    
    [Fact]
    public async Task Handle_InvalidSquadAssociateId_ReturnsValidationResult()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<TrashSquadAssociateCommand>();
        _reader.PickAsync(Arg.Any<SquadAssociateId>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(default(SquadAssociate)));
            
        // Act
        var sut = _serviceProvider.GetService<TrashSquadAssociateCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(true), CancellationToken.None);
        
        // Assert
        result.FirstError.Code.Should().Be("squad_associate.not_found");
    }
    
    [Fact]
    public async Task Handle_ValidRequest_ReturnsTrue()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<TrashSquadAssociateCommand>();
        var entity = fixture.Create<SquadAssociate>();
        _reader.PickAsync(Arg.Any<SquadAssociateId>(), Arg.Any<CancellationToken>())!
            .Returns(Task.FromResult(entity));
            
        // Act
        var sut = _serviceProvider.GetService<TrashSquadAssociateCommandValidationBehavior>();
        var result = await sut!.Handle(command, 
            async () => await Task.FromResult(true), CancellationToken.None);
        
        // Assert
        result.Value.Should().BeTrue();
    }
}