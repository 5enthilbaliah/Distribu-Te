﻿namespace DistribuTe.Mutators.Teams.UnitTests.Infrastructure.Persistence;

using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MockQueryable.NSubstitute;
using NSubstitute;
using Teams.Domain.Entities;
using Teams.Infrastructure.Persistence;

public class AssociateEntityRepositoryTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly TeamSchemaDatabaseContext _dbContext = Substitute.For<TeamSchemaDatabaseContext>();

    public AssociateEntityRepositoryTests()
    {
        var services = new ServiceCollection();
        services.AddScoped<TeamSchemaDatabaseContext>(_ => _dbContext);
        services.AddScoped<AssociateEntityRepository>();
        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    public void SpawnOne_AddsAssociate()
    {
        // Arrange
        var fixture = new Fixture { RepeatCount = 10 };
        var entityBuilder = fixture.Build<Associate>();
        var entities = fixture.Repeat(entityBuilder.Create).ToList();
        
        var mockDbSet = entities.AsQueryable().BuildMockDbSet();
        _dbContext.Set<Associate>().Returns(mockDbSet);
        
        // Act
        var sut = _serviceProvider.GetService<AssociateEntityRepository>();
        sut!.SpawnOne(fixture.Create<Associate>());
        
        // Assert
        mockDbSet.Received().Add(Arg.Any<Associate>());
    }
    
    [Fact]
    public void CommitOne_UpdatesAssociate()
    {
        // Arrange
        var fixture = new Fixture { RepeatCount = 10 };
        var entityBuilder = fixture.Build<Associate>();
        var entities = fixture.Repeat(entityBuilder.Create).ToList();
        
        var mockDbSet = entities.AsQueryable().BuildMockDbSet();
        _dbContext.Set<Associate>().Returns(mockDbSet);
        
        // Act
        var sut = _serviceProvider.GetService<AssociateEntityRepository>();
        sut!.CommitOne(fixture.Create<Associate>());
        
        // Assert
        mockDbSet.Received().Update(Arg.Any<Associate>());
    }
    
    [Fact]
    public void TrashOne_RemovesAssociate()
    {
        // Arrange
        var fixture = new Fixture { RepeatCount = 10 };
        var entityBuilder = fixture.Build<Associate>();
        var entities = fixture.Repeat(entityBuilder.Create).ToList();
        
        var mockDbSet = entities.AsQueryable().BuildMockDbSet();
        _dbContext.Set<Associate>().Returns(mockDbSet);
        
        // Act
        var sut = _serviceProvider.GetService<AssociateEntityRepository>();
        sut!.TrashOne(fixture.Create<Associate>());
        
        // Assert
        mockDbSet.Received().Remove(Arg.Any<Associate>());
    }

    [Fact]
    public async Task PickAsync_ReturnsAssociate()
    {
        // Arrange
        var fixture = new Fixture { RepeatCount = 10 };
        var entityBuilder = fixture.Build<Associate>();
        var entities = fixture.Repeat(entityBuilder.Create).ToList();
        
        var mockDbSet = entities.AsQueryable().BuildMockDbSet();
        _dbContext.Set<Associate>().Returns(mockDbSet);
        
        // Act
        var sut = _serviceProvider.GetService<AssociateEntityRepository>();
        var actual = await sut!.PickAsync(entities[4].Id, CancellationToken.None);
        
        // Assert
        actual.Should().NotBeNull();
    }

    [Fact]
    public async Task AnyAsync_ReturnsTrue()
    {
        // Arrange
        var fixture = new Fixture { RepeatCount = 10 };
        var entityBuilder = fixture.Build<Associate>();
        var entities = fixture.Repeat(entityBuilder.Create).ToList();
        
        var mockDbSet = entities.AsQueryable().BuildMockDbSet();
        _dbContext.Set<Associate>().Returns(mockDbSet);
        
        // Act
        var sut = _serviceProvider.GetService<AssociateEntityRepository>();
        var found = await sut!.AnyAsync(x => x.Id == entities[4].Id, CancellationToken.None);
        
        // Assert
        found.Should().BeTrue();
    }
}