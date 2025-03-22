namespace DistribuTe.Mutators.Projects.Infrastructure.Persistence;

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

    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<ProjectCategory> ProjectCategories { get; set; }
    public virtual DbSet<SquadProject> SquadProjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddConfiguration<ProjectConfiguration, Project>();
        modelBuilder.AddConfiguration<ProjectCategoryConfiguration, ProjectCategory>();
        modelBuilder.AddConfiguration<SquadProjectConfiguration, SquadProject>();
    }
}