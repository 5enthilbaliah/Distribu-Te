namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using Domain.Entities;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Specifications;

public class TeamDatabaseContext(DbContextOptions<TeamDatabaseContext> options) : DbContext(options)
{
    public DbSet<Associate> Associates { get; set; }
    public DbSet<Squad> Squads { get; set; }
    public DbSet<SquadAssociate> SquadAssociates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddConfiguration<AssociateConfiguration, Associate>();
        modelBuilder.AddConfiguration<SquadConfiguration, Squad>();
        modelBuilder.AddConfiguration<SquadAssociateConfiguration, SquadAssociate>();
    }
}