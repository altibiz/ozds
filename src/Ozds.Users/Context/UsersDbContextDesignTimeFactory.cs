using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ozds.Users.Context;

public class UsersDbContextDesignTimeFactory
  : IDesignTimeDbContextFactory<UsersDbContext>
{
  public UsersDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<UsersDbContext>();

    optionsBuilder
      .UseNpgsql(
        "Server=localhost;Port=5432;User Id=ozds;Password=ozds;Database=ozds",
        x =>
        {
          x.MigrationsAssembly(typeof(UsersDbContext).Assembly.GetName().Name);
          x.MigrationsHistoryTable($"__Ozds{nameof(UsersDbContext)}");
        }
      )
      .UseSnakeCaseNamingConvention();

    return new UsersDbContext(optionsBuilder.Options);
  }
}
