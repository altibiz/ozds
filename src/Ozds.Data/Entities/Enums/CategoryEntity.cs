using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Enums;

public enum CategoryEntity
{
  All,
  Messenger,
  MessengerPush,
  Audit,
  Error,
  Lifecycle
}

public class CategoryEntityModelConfiguration : IModelConfiguration
{
  public void Configure(ModelBuilder modelBuilder)
  {
    modelBuilder.HasPostgresEnum<CategoryEntity>();
  }
}

public class
  CategoryEntityNpgsqlDataSourceConfiguration : INpgsqlDataSourceConfiguration
{
  public void Configure(NpgsqlDataSourceBuilder builder)
  {
    builder.MapEnum<CategoryEntity>();
  }
}
