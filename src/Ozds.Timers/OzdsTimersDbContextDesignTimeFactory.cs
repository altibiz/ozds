using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ozds.Timers;

public class OzdsTimersDbContextDesignTimeFactory
  : IDesignTimeDbContextFactory<OzdsTimersDbContext>
{
  public OzdsTimersDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<OzdsTimersDbContext>();
    optionsBuilder
      .UseNpgsql(
        "Server=localhost;Port=5432;User Id=ozds;Password=ozds;Database=ozds",
        x =>
        {
          x.MigrationsAssembly(
            typeof(OzdsTimersDbContext).Assembly.GetName().Name);
          x.MigrationsHistoryTable(
            $"__{nameof(OzdsTimersDbContext)}");
        });
    return new OzdsTimersDbContext(optionsBuilder.Options);
  }
}
