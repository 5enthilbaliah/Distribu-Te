namespace DistribuTe.Mutators.Teams.UnitTests.Infrastructure.Persistence;

using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MockQueryable.NSubstitute;
using NSubstitute;
using Teams.Domain.Entities;
using Teams.Infrastructure.Persistence;

public class SquadTeamsRepositoryTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly TeamDatabaseContext _dbContext = Substitute.For<TeamDatabaseContext>();

    public SquadTeamsRepositoryTests()
    {
        var services = new ServiceCollection();
        services.AddScoped<TeamDatabaseContext>(_ => _dbContext);
        services.AddScoped<SquadTeamsRepository>();
        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    public void SpawnOne_AddsSquad()
    {
        // Arrange
        var fixture = new Fixture { RepeatCount = 10 };
        var entityBuilder = fixture.Build<Squad>();
        var entities = fixture.Repeat(entityBuilder.Create).ToList();
        
        var mockDbSet = entities.AsQueryable().BuildMockDbSet();
        _dbContext.Set<Squad>().Returns(mockDbSet);
        
        // Act
        var sut = _serviceProvider.GetService<SquadTeamsRepository>();
        sut!.SpawnOne(fixture.Create<Squad>());
        
        // Assert
        mockDbSet.Received().Add(Arg.Any<Squad>());
    }
    
    [Fact]
    public void CommitOne_UpdatesSquad()
    {
        // Arrange
        var fixture = new Fixture { RepeatCount = 10 };
        var entityBuilder = fixture.Build<Squad>();
        var entities = fixture.Repeat(entityBuilder.Create).ToList();
        
        var mockDbSet = entities.AsQueryable().BuildMockDbSet();
        _dbContext.Set<Squad>().Returns(mockDbSet);
        
        // Act
        var sut = _serviceProvider.GetService<SquadTeamsRepository>();
        sut!.CommitOne(fixture.Create<Squad>());
        
        // Assert
        mockDbSet.Received().Update(Arg.Any<Squad>());
    }
    
    [Fact]
    public void TrashOne_RemovesSquad()
    {
        // Arrange
        var fixture = new Fixture { RepeatCount = 10 };
        var entityBuilder = fixture.Build<Squad>();
        var entities = fixture.Repeat(entityBuilder.Create).ToList();
        
        var mockDbSet = entities.AsQueryable().BuildMockDbSet();
        _dbContext.Set<Squad>().Returns(mockDbSet);
        
        // Act
        var sut = _serviceProvider.GetService<SquadTeamsRepository>();
        sut!.TrashOne(fixture.Create<Squad>());
        
        // Assert
        mockDbSet.Received().Remove(Arg.Any<Squad>());
    }

    [Fact]
    public async Task PickAsync_ReturnsSquad()
    {
        // Arrange
        var fixture = new Fixture { RepeatCount = 10 };
        var entityBuilder = fixture.Build<Squad>();
        var entities = fixture.Repeat(entityBuilder.Create).ToList();
        
        var mockDbSet = entities.AsQueryable().BuildMockDbSet();
        _dbContext.Set<Squad>().Returns(mockDbSet);
        
        // Act
        var sut = _serviceProvider.GetService<SquadTeamsRepository>();
        var actual = await sut!.PickAsync(entities[4].Id, CancellationToken.None);
        
        // Assert
        actual.Should().NotBeNull();
    }

    [Fact]
    public async Task AnyAsync_ReturnsTrue()
    {
        // Arrange
        var fixture = new Fixture { RepeatCount = 10 };
        var entityBuilder = fixture.Build<Squad>();
        var entities = fixture.Repeat(entityBuilder.Create).ToList();
        
        var mockDbSet = entities.AsQueryable().BuildMockDbSet();
        _dbContext.Set<Squad>().Returns(mockDbSet);
        
        // Act
        var sut = _serviceProvider.GetService<SquadTeamsRepository>();
        var found = await sut!.AnyAsync(x => x.Id == entities[4].Id, CancellationToken.None);
        
        // Assert
        found.Should().BeTrue();
    }
}