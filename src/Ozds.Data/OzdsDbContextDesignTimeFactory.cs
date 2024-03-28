using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Ozds.Data.Extensions;

namespace Ozds.Data;

public class BloggingContextFactory : IDesignTimeDbContextFactory<OzdsDbContext>
{
  public OzdsDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<OzdsDbContext>();
    optionsBuilder.UseTimescale(
      "Server=localhost;Port=5432;User Id=ozds;Password=ozds;Database=ozds");
    return new OzdsDbContext(optionsBuilder.Options);
  }
}
