// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Projects.Infrastructure.Persistence._helpers;

using Microsoft.EntityFrameworkCore;

public static class EntityTypeConfigurationExtensions
{
    public static void AddConfiguration<TConf, TEntity>(this ModelBuilder modelBuilder) 
        where TEntity : class
        where TConf : class, IEntityTypeConfiguration<TEntity>, new()
    {
        var configuration = new TConf();
        configuration.Configure(modelBuilder.Entity<TEntity>());
    }
}