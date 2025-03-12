namespace DistribuTe.Mutators.Teams.UnitTests.Infrastructure.Persistence;

using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Teams.Application.Shared;
using Teams.Domain.Entities;
using Teams.Infrastructure.Persistence;

public class UnitOfWorkTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly IDateTimeProvider  _dateTimeProvider = Substitute.For<IDateTimeProvider>();

    public UnitOfWorkTests()
    {
        var services = new ServiceCollection();
        services.AddDbContext<TeamDatabaseContext>(opt => opt.UseInMemoryDatabase(databaseName: "TeamDatabase"));
        services.AddScoped<UnitOfWork>();
        services.AddScoped(_ => _dateTimeProvider);
        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    public async Task SaveChangesAsync_UpdatesAudit()
    {
        // Arrange
        var utcNow = DateTime.UtcNow;
        var fixture = new Fixture { RepeatCount = 10 };
        var entityBuilder = fixture.Build<Associate>();
        var entities = fixture.Repeat(entityBuilder.Create).ToList();
        _dateTimeProvider.UtcNow.Returns(utcNow);
        
        // Act
        var dbContext = _serviceProvider.GetRequiredService<TeamDatabaseContext>();
        var uow = _serviceProvider.GetRequiredService<UnitOfWork>();
        foreach (var associate in entities)
        {
            dbContext.Add(associate);
        }
        
        await uow.SaveChangesAsync("test-user", CancellationToken.None);

        entities[6].LastName = "Mutate Now!!!";
        await uow.SaveChangesAsync("test-user", CancellationToken.None);
        
        // Assert
        foreach (var associate in entities)
        {
            associate.CreatedBy.Should().Be("test-user");
            associate.CreatedOn.Should().Be(utcNow);
        }
        
        entities[6].ModifiedBy.Should().Be("test-user");
        entities[6].ModifiedOn.Should().Be(utcNow);
    }
}