using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ozds.Data.Context;

namespace Ozds.Data.Entities.Enums;

public enum ActionEntity
{
  Read,
  List,
  Create,
  Update,
  Delete,
  Restore,
  Forget
}

public class ActionEntityTypeConfiguration : IModelConfiguration
{
  public void Configure(ModelBuilder modelBuilder)
  {
    modelBuilder.HasPostgresEnum<ActionEntity>();
  }
}

public class
  ActionEntityNpgsqlDataSourceConfiguration : INpgsqlDataSourceConfiguration
{
  public void Configure(NpgsqlDataSourceBuilder builder)
  {
    builder.MapEnum<ActionEntity>();
  }
}
