namespace DistribuTe.Infrastructure.AppDatabase;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class DistribuTeDbContextDesignFactory : IDesignTimeDbContextFactory<DistribuTeDbContext>
{
    public DistribuTeDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DistribuTeDbContext>();
        optionsBuilder.UseSqlServer("", opts =>
            opts.MigrationsAssembly(typeof(DistribuTeDbContextDesignFactory).Assembly.FullName));

        return new DistribuTeDbContext(optionsBuilder.Options);
    }
}