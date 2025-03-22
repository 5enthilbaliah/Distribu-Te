namespace DistribuTe.Aggregates.Teams.Infrastructure.Persistence;

using Domain.Entities;
using Framework.InfrastructureEssentials.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Specifications;

public class TeamSchemaDatabaseContext : DbContext
{
    // Needed for unit testing
    public TeamSchemaDatabaseContext()
    {}

    public TeamSchemaDatabaseContext(DbContextOptions<TeamSchemaDatabaseContext> options)
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