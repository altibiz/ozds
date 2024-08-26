using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ozds.Jobs;

public class OzdsJobsDbContextDesignTimeFactory
  : IDesignTimeDbContextFactory<OzdsJobsDbContext>
{
  public OzdsJobsDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<OzdsJobsDbContext>();
    optionsBuilder
      .UseNpgsql(
        "Server=localhost;Port=5432;User Id=ozds;Password=ozds;Database=ozds",
        x =>
        {
          x.MigrationsAssembly(
            typeof(OzdsJobsDbContext).Assembly.GetName().Name);
          x.MigrationsHistoryTable(
            $"__{nameof(OzdsJobsDbContext)}");
        });
    return new OzdsJobsDbContext(optionsBuilder.Options);
  }
}
