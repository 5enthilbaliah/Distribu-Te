namespace DistribuTe.Mutators.Teams.UnitTests.Application.SquadAssociates;

using AutoFixture;
using FluentAssertions;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Teams.Application;
using Teams.Application.Shared;
using Teams.Application.SquadAssociates;
using Teams.Application.SquadAssociates.Mappings;
using Teams.Domain.Entities;

public class CommandHandlerTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly ITeamsMutator<SquadAssociate, SquadAssociateId> _teamsMutator = 
        Substitute.For<ITeamsMutator<SquadAssociate, SquadAssociateId>>();
    private readonly IExistingEntityMarker<SquadAssociate, SquadAssociateId> _entityMarker =
        Substitute.For<IExistingEntityMarker<SquadAssociate, SquadAssociateId>>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    public CommandHandlerTests()
    {
        var services = new ServiceCollection();
        
        services.AddScoped(_ => _teamsMutator);
        services.AddScoped(_ => _entityMarker);
        services.AddScoped(_ => _unitOfWork);
        var mapsterConfig = TypeAdapterConfig.GlobalSettings;
        var config = new SquadAssociateMapperConfig();
        config.Register(mapsterConfig);
        services.AddSingleton(mapsterConfig);
        services.AddMapster();
        services.AddScoped<CommandHandler>();
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    [Fact]
    public void CommandHandler_NullArgument_ThrowsArgumentNullException()
    {
        var mapper = _serviceProvider.GetService<IMapper>();
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Action act = () => new CommandHandler(null, _entityMarker, _unitOfWork, mapper!);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'mutator')");
        
        act = () => new CommandHandler(_teamsMutator, null, _unitOfWork, mapper!);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'entityMarker')");
        
        act = () => new CommandHandler(_teamsMutator, _entityMarker, null, mapper!);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'unitOfWork')");
        
        act = () => new CommandHandler(_teamsMutator, _entityMarker, _unitOfWork, null);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'mapper')");
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
    
    [Fact]
    public async Task Handle_SpawnSquadAssociateCommand_ReturnsSquadAssociateResponse()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<SpawnSquadAssociateCommand>();

        _teamsMutator.SpawnOne(Arg.Any<SquadAssociate>());
        _unitOfWork.SaveChangesAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);
        
        // Act
        var sut = _serviceProvider.GetService<CommandHandler>();
        var result = await sut!.Handle(command, CancellationToken.None);
        
        // Assert
        result.Value.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Handle_CommitSquadAssociateCommand_ReturnsSquadAssociateResponse()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<CommitSquadAssociateCommand>();
        var entity = fixture.Create<SquadAssociate>();
        entity.Id = new SquadAssociateId(entity.SquadId, entity.AssociateId);

        _entityMarker.Entity.Returns(entity);
        _entityMarker.Id.Returns(entity.Id);
        _teamsMutator.CommitOne(Arg.Any<SquadAssociate>());
        _unitOfWork.SaveChangesAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);
        
        // Act
        var sut = _serviceProvider.GetService<CommandHandler>();
        var result = await sut!.Handle(command, CancellationToken.None);
        
        // Assert
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task Handle_TrashSquadAssociateCommand_ReturnsTrue()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<TrashSquadAssociateCommand>();
        var entity = fixture.Create<SquadAssociate>();
        
        _entityMarker.Entity.Returns(entity);
        _teamsMutator.TrashOne(Arg.Any<SquadAssociate>());
        _unitOfWork.SaveChangesAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);
        
        // Act
        var sut = _serviceProvider.GetService<CommandHandler>();
        var result = await sut!.Handle(command, CancellationToken.None);
        
        // Assert
        result.Value.Should().BeTrue();
    }
}