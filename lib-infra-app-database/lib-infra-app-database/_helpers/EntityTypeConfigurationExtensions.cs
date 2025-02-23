// ReSharper disable CheckNamespace
namespace DistribuTe.Infrastructure.AppDatabase.Helpers;

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