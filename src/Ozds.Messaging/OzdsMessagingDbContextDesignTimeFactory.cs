using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ozds.Messaging;

public class OzdsMessagingDbContextDesignTimeFactory
  : IDesignTimeDbContextFactory<OzdsMessagingDbContext>
{
  public OzdsMessagingDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<OzdsMessagingDbContext>();
    optionsBuilder
      .UseNpgsql(
        "Server=localhost;Port=5432;User Id=ozds;Password=ozds;Database=ozds",
        x =>
        {
          x.MigrationsAssembly(
            typeof(OzdsMessagingDbContext).Assembly.GetName().Name);
          x.MigrationsHistoryTable(
            $"__{nameof(OzdsMessagingDbContext)}");
        });
    return new OzdsMessagingDbContext(optionsBuilder.Options);
  }
}
