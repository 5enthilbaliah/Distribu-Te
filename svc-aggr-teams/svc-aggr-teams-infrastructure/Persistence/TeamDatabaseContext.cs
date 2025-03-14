namespace DistribuTe.Aggregates.Teams.Infrastructure.Persistence;

using Domain.Entities;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Specifications;

public class TeamDatabaseContext : DbContext
{
    // Needed for unit testing
    public TeamDatabaseContext()
    {}

    public TeamDatabaseContext(DbContextOptions<TeamDatabaseContext> options)
        : base(options)
    { }
    
    public virtual DbSet<AssociateAggregate> Associates { get; set; }
    public virtual DbSet<SquadAggregate> Squads { get; set; }
    public virtual DbSet<SquadAssociateAggregate> SquadAssociates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddConfiguration<AssociateAggregateConfiguration, AssociateAggregate>();
        modelBuilder.AddConfiguration<SquadAggregateConfiguration, SquadAggregate>();
        modelBuilder.AddConfiguration<SquadAssociateAggregateConfiguration, SquadAssociateAggregate>();
    }
}