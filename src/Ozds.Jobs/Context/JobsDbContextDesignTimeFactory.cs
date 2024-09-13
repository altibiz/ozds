using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ozds.Jobs.Context;

public class JobsDbContextDesignTimeFactory
  : IDesignTimeDbContextFactory<JobsDbContext>
{
  public JobsDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<JobsDbContext>();
    optionsBuilder
      .UseNpgsql(
        "Server=localhost;Port=5432;User Id=ozds;Password=ozds;Database=ozds",
        x =>
        {
          x.MigrationsAssembly(
            typeof(JobsDbContext).Assembly.GetName().Name);
          x.MigrationsHistoryTable(
            $"__Ozds{nameof(JobsDbContext)}");
        });
    return new JobsDbContext(optionsBuilder.Options);
  }
}
