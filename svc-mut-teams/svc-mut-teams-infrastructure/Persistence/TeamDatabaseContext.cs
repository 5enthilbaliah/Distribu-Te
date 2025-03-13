namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

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

    public virtual DbSet<Associate> Associates { get; set; }
    public virtual DbSet<Squad> Squads { get; set; }
    public virtual DbSet<SquadAssociate> SquadAssociates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddConfiguration<AssociateConfiguration, Associate>();
        modelBuilder.AddConfiguration<SquadConfiguration, Squad>();
        modelBuilder.AddConfiguration<SquadAssociateConfiguration, SquadAssociate>();
    }
}