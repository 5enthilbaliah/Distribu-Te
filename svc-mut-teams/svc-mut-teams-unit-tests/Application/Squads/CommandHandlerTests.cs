﻿namespace DistribuTe.Mutators.Teams.UnitTests.Application.Squads;

using AutoFixture;
using FluentAssertions;
using Framework.AppEssentials;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Teams.Application;
using Teams.Application.Squads;
using Teams.Application.Squads.Mappers;
using Teams.Domain.Entities;

public class CommandHandlerTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly IEntityMutator<Squad, SquadId> _entityMutator = 
        Substitute.For<IEntityMutator<Squad, SquadId>>();
    private readonly IExistingEntityMarker<Squad, SquadId> _entityMarker =
        Substitute.For<IExistingEntityMarker<Squad, SquadId>>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    public CommandHandlerTests()
    {
        var services = new ServiceCollection();
        
        services.AddScoped(_ => _entityMutator);
        services.AddScoped(_ => _entityMarker);
        services.AddScoped(_ => _unitOfWork);
        var mapsterConfig = TypeAdapterConfig.GlobalSettings;
        var config = new SquadMapperConfig();
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
        
        act = () => new CommandHandler(_entityMutator, null, _unitOfWork, mapper!);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'entityMarker')");
        
        act = () => new CommandHandler(_entityMutator, _entityMarker, null, mapper!);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'unitOfWork')");
        
        act = () => new CommandHandler(_entityMutator, _entityMarker, _unitOfWork, null);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'mapper')");
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
    
    [Fact]
    public async Task Handle_SpawnSquadCommand_ReturnsSquadResponse()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<SpawnSquadCommand>();

        _entityMutator.SpawnOne(Arg.Any<Squad>());
        _unitOfWork.SaveChangesAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);
        
        // Act
        var sut = _serviceProvider.GetService<CommandHandler>();
        var result = await sut!.Handle(command, CancellationToken.None);
        
        // Assert
        result.Value.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Handle_CommitSquadCommand_ReturnsSquadResponse()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<CommitSquadCommand>();
        var entity = fixture.Create<Squad>();

        _entityMarker.Entity.Returns(entity);
        _entityMutator.CommitOne(Arg.Any<Squad>());
        _unitOfWork.SaveChangesAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);
        
        // Act
        var sut = _serviceProvider.GetService<CommandHandler>();
        var result = await sut!.Handle(command, CancellationToken.None);
        
        // Assert
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task Handle_TrashSquadCommand_ReturnsTrue()
    {
        // Arrange
        var fixture = new Fixture();
        var command = fixture.Create<TrashSquadCommand>();
        var entity = fixture.Create<Squad>();
        
        _entityMarker.Entity.Returns(entity);
        _entityMutator.TrashOne(Arg.Any<Squad>());
        _unitOfWork.SaveChangesAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);
        
        // Act
        var sut = _serviceProvider.GetService<CommandHandler>();
        var result = await sut!.Handle(command, CancellationToken.None);
        
        // Assert
        result.Value.Should().BeTrue();
    }
}