namespace DistribuTe.Aggregates.Projects.Infrastructure.Persistence;

using Domain.Entities;
using Framework.InfrastructureEssentials.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Specifications;

public class ProjectSchemaDatabaseContext : DbContext
{
    // Needed for unit testing
    public ProjectSchemaDatabaseContext()
    {}

    public ProjectSchemaDatabaseContext(DbContextOptions<ProjectSchemaDatabaseContext> options)
        : base(options)
    { }
    
    public virtual DbSet<ProjectAggregate> Projects { get; set; }
    public virtual DbSet<ProjectCategoryAggregate> ProjectCategories { get; set; }
    public virtual DbSet<SquadProjectAggregate> SquadProjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddConfiguration<ProjectAggregateConfiguration, ProjectAggregate>();
        modelBuilder.AddConfiguration<ProjectCategoryAggregateConfiguration, ProjectCategoryAggregate>();
        modelBuilder.AddConfiguration<SquadProjectAggregateConfiguration, SquadProjectAggregate>();
    }
}