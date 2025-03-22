namespace DistribuTe.Mutators.Teams.UnitTests.Infrastructure.Persistence;

using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MockQueryable.NSubstitute;
using NSubstitute;
using Teams.Domain.Entities;
using Teams.Infrastructure.Persistence;

public class SquadAssociateEntityRepositoryTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly TeamSchemaDatabaseContext _dbContext = Substitute.For<TeamSchemaDatabaseContext>();

    public SquadAssociateEntityRepositoryTests()
    {
        var services = new ServiceCollection();
        services.AddScoped<TeamSchemaDatabaseContext>(_ => _dbContext);
        services.AddScoped<SquadAssociateEntityRepository>();
        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    public void SpawnOne_AddsSquadAssociate()
    {
        // Arrange
        var fixture = new Fixture { RepeatCount = 10 };
        var entityBuilder = fixture.Build<SquadAssociate>();
        var entities = fixture.Repeat(entityBuilder.Create).ToList();
        
        var mockDbSet = entities.AsQueryable().BuildMockDbSet();
        _dbContext.Set<SquadAssociate>().Returns(mockDbSet);
        
        // Act
        var sut = _serviceProvider.GetService<SquadAssociateEntityRepository>();
        sut!.SpawnOne(fixture.Create<SquadAssociate>());
        
        // Assert
        mockDbSet.Received().Add(Arg.Any<SquadAssociate>());
    }
    
    [Fact]
    public void CommitOne_UpdatesSquadAssociate()
    {
        // Arrange
        var fixture = new Fixture { RepeatCount = 10 };
        var entityBuilder = fixture.Build<SquadAssociate>();
        var entities = fixture.Repeat(entityBuilder.Create).ToList();
        
        var mockDbSet = entities.AsQueryable().BuildMockDbSet();
        _dbContext.Set<SquadAssociate>().Returns(mockDbSet);
        
        // Act
        var sut = _serviceProvider.GetService<SquadAssociateEntityRepository>();
        sut!.CommitOne(fixture.Create<SquadAssociate>());
        
        // Assert
        mockDbSet.Received().Update(Arg.Any<SquadAssociate>());
    }
    
    [Fact]
    public void TrashOne_RemovesSquadAssociate()
    {
        // Arrange
        var fixture = new Fixture { RepeatCount = 10 };
        var entityBuilder = fixture.Build<SquadAssociate>();
        var entities = fixture.Repeat(entityBuilder.Create).ToList();
        
        var mockDbSet = entities.AsQueryable().BuildMockDbSet();
        _dbContext.Set<SquadAssociate>().Returns(mockDbSet);
        
        // Act
        var sut = _serviceProvider.GetService<SquadAssociateEntityRepository>();
        sut!.TrashOne(fixture.Create<SquadAssociate>());
        
        // Assert
        mockDbSet.Received().Remove(Arg.Any<SquadAssociate>());
    }

    [Fact]
    public async Task AnyAsync_ReturnsTrue()
    {
        // Arrange
        var fixture = new Fixture { RepeatCount = 10 };
        var entityBuilder = fixture.Build<SquadAssociate>();
        var entities = fixture.Repeat(entityBuilder.Create).ToList();
        
        var mockDbSet = entities.AsQueryable().BuildMockDbSet();
        _dbContext.Set<SquadAssociate>().Returns(mockDbSet);
        
        // Act
        var sut = _serviceProvider.GetService<SquadAssociateEntityRepository>();
        var found = await sut!.AnyAsync(x => x.SquadId == entities[4].SquadId, CancellationToken.None);
        
        // Assert
        found.Should().BeTrue();
    }
}