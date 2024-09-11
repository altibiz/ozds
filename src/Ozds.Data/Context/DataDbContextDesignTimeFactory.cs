using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;
using Ozds.Data.Extensions;

namespace Ozds.Data.Context;

public class DataDbContextDesignTimeFactory
  : IDesignTimeDbContextFactory<DataDbContext>
{
  public DataDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();

    var dataSourceBuilder =
      new NpgsqlDataSourceBuilder(
        "Server=localhost;Port=5432;User Id=ozds;Password=ozds;Database=ozds");
    dataSourceBuilder.ApplyConfigurationsFromAssembly(
      typeof(DataDbContext).Assembly);

    optionsBuilder.UseTimescale(
      dataSourceBuilder.Build(),
      x =>
      {
        x.MigrationsAssembly(
          typeof(DataDbContext).Assembly.GetName().Name);
        x.MigrationsHistoryTable(
          $"__{nameof(DataDbContext)}");
      });
    return new DataDbContext(optionsBuilder.Options);
  }
}
