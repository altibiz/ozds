using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Ozds.Data.Extensions;

namespace Ozds.Data;

public class OzdsDataDbContextDesignTimeFactory
  : IDesignTimeDbContextFactory<OzdsDataDbContext>
{
  public OzdsDataDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<OzdsDataDbContext>();
    optionsBuilder.UseTimescale(
      "Server=localhost;Port=5432;User Id=ozds;Password=ozds;Database=ozds",
      x =>
      {
        x.MigrationsAssembly(
          typeof(OzdsDataDbContext).Assembly.GetName().Name);
        x.MigrationsHistoryTable(
          $"__{nameof(OzdsDataDbContext)}");
      });
    return new OzdsDataDbContext(optionsBuilder.Options);
  }
}
