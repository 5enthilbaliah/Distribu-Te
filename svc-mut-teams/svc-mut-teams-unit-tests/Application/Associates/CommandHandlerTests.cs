namespace DistribuTe.Mutators.Teams.UnitTests.Application.Associates;

using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Teams.Application;
using Teams.Application.Associates;
using Teams.Application.Associates.Mappings;
using Teams.Application.Shared;

public class CommandHandlerTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly ITeamsMutator<Associate, AssociateId> _teamsMutator = 
        Substitute.For<ITeamsMutator<Associate, AssociateId>>();
    private readonly IExistingEntityMarker<Associate, AssociateId> _entityMarker =
        Substitute.For<IExistingEntityMarker<Associate, AssociateId>>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    public CommandHandlerTests()
    {
        var services = new ServiceCollection();
        
        services.AddScoped(_ => _teamsMutator);
        services.AddScoped(_ => _entityMarker);
        services.AddScoped(_ => _unitOfWork);
        var mapsterConfig = TypeAdapterConfig.GlobalSettings;
        var config = new AssociateMapperConfig();
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
    public async Task Handle_SpawnAssociateCommand_ReturnsAssociateResponse()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<SpawnAssociateCommand>();

        _teamsMutator.SpawnOne(Arg.Any<Associate>());
        _unitOfWork.SaveChangesAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);
        
        // Act
        var sut = _serviceProvider.GetService<CommandHandler>();
        var result = await sut!.Handle(command, CancellationToken.None);
        
        // Assert
        result.Value.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Handle_CommitAssociateCommand_ReturnsAssociateResponse()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<CommitAssociateCommand>();
        var entity = fixture.Create<Associate>();

        _entityMarker.Entity.Returns(entity);
        _teamsMutator.CommitOne(Arg.Any<Associate>());
        _unitOfWork.SaveChangesAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);
        
        // Act
        var sut = _serviceProvider.GetService<CommandHandler>();
        var result = await sut!.Handle(command, CancellationToken.None);
        
        // Assert
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task Handle_TrashAssociateCommand_ReturnsTrue()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<TrashAssociateCommand>();
        var entity = fixture.Create<Associate>();
        
        _entityMarker.Entity.Returns(entity);
        _teamsMutator.TrashOne(Arg.Any<Associate>());
        _unitOfWork.SaveChangesAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);
        
        // Act
        var sut = _serviceProvider.GetService<CommandHandler>();
        var result = await sut!.Handle(command, CancellationToken.None);
        
        // Assert
        result.Value.Should().BeTrue();
    }
}