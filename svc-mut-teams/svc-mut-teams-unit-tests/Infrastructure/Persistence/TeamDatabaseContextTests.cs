namespace DistribuTe.Mutators.Teams.UnitTests.Infrastructure.Persistence;

using AutoFixture;
using AutoFixture.Dsl;
using FluentAssertions;
using Framework.DomainEssentials;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Teams.Domain.Entities;
using Teams.Infrastructure.Persistence;

public class TeamDatabaseContextTests
{
    private readonly ServiceProvider _serviceProvider;

    public TeamDatabaseContextTests()
    {
        var services = new ServiceCollection();
        services.AddDbContext<TeamDatabaseContext>(opt => opt.UseInMemoryDatabase(databaseName: "TeamDatabase"));
        
        _serviceProvider = services.BuildServiceProvider();
    }

    private async Task DbSet_Validate<TEntity, TId>(Func<ICustomizationComposer<TEntity>, IPostprocessComposer<TEntity>>? composer = null,
        CancellationToken cancellationToken = default)
        where TEntity : class, IEntity<TId>
        where TId : class
    {
        // Arrange
        var fixture = new Fixture { RepeatCount = 10 };
        var entityBuilder = composer is null ?
            fixture.Build<TEntity>() : composer?.Invoke(fixture.Build<TEntity>());
        var entities = fixture.Repeat(entityBuilder.Create).ToList();
        
        // Act
        var sut = _serviceProvider.GetService<TeamDatabaseContext>();
        foreach (var entity in entities)
        {
            sut!.Add(entity);
        }
        await sut!.SaveChangesAsync(cancellationToken);
        var results = await sut.Set<TEntity>().ToListAsync(cancellationToken);
        
        // Assert
        results.Count.Should().BeGreaterThanOrEqualTo(10);
        
        foreach (var entity in entities)
        {
            sut!.Remove(entity);
        }
        await sut!.SaveChangesAsync(cancellationToken);
    }

    [Fact]
    public async Task Associates_ReturnsDbSet()
        => await DbSet_Validate<Associate, AssociateId>(cancellationToken: CancellationToken.None);
    
    [Fact]
    public async Task Squads_ReturnsDbSet()
        => await DbSet_Validate<Squad, SquadId>(cancellationToken: CancellationToken.None);
    
    [Fact]
    public async Task SquadAssociates_ReturnsDbSet()
        => await DbSet_Validate<SquadAssociate, SquadAssociateId>(cancellationToken: CancellationToken.None);
}