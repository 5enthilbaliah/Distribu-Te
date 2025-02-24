#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DistribuTe.Infrastructure.AppDatabase;

using Domain.AppEntities;
using Domain.AppEntities.Specifications;
using Helpers;
using Microsoft.EntityFrameworkCore;

public class DistribuTeDbContext(DbContextOptions<DistribuTeDbContext> options) : DbContext(options)
{
    public virtual DbSet<ProjectCategoryAggregate> ProjectCategories { get; set; }
    public virtual DbSet<ProjectAggregate> Projects { get; set; }
    public virtual DbSet<SquadAggregate> Squads { get; set; }
    public virtual DbSet<AssociateAggregate> Associates { get; set; }
    public virtual DbSet<SquadAssociateAggregate> SquadAssociates { get; set; }
    public virtual DbSet<SquadProjectAggregate> SquadProjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddConfiguration<ProjectCategoryAggregateConfiguration, ProjectCategoryAggregate>();
        modelBuilder.AddConfiguration<ProjectAggregateConfiguration, ProjectAggregate>();
        modelBuilder.AddConfiguration<SquadAggregateConfiguration, SquadAggregate>();
        modelBuilder.AddConfiguration<AssociateAggregateConfiguration, AssociateAggregate>();
        modelBuilder.AddConfiguration<SquadAssociateAggregateConfiguration, SquadAssociateAggregate>();
        modelBuilder.AddConfiguration<SquadProjectAggregateConfiguration, SquadProjectAggregate>();
    }
}