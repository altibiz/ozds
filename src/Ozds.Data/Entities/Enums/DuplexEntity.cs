using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ozds.Data.Context;

namespace Ozds.Data.Entities.Enums;

public enum DuplexEntity
{
  Any,
  Net,
  Import,
  Export,
}

public class DuplexEntityTypeConfiguration : IModelConfiguration
{
  public void Configure(ModelBuilder modelBuilder)
  {
    modelBuilder.HasPostgresEnum<DuplexEntity>();
  }
}

public class DuplexEntityNpgsqlDataSourceConfiguration
  : INpgsqlDataSourceConfiguration
{
  public void Configure(NpgsqlDataSourceBuilder builder)
  {
    builder.MapEnum<DuplexEntity>();
  }
}
