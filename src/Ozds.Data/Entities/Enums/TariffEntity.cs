using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ozds.Data.Context;

namespace Ozds.Data.Entities.Enums;

public enum TariffEntity
{
  T0,
  T1,
  T2
}

public class TariffEntityTypeConfiguration : IModelConfiguration
{
  public void Configure(ModelBuilder modelBuilder)
  {
    modelBuilder.HasPostgresEnum<TariffEntity>();
  }
}

public class
  TariffEntityNpgsqlDataSourceConfiguration : INpgsqlDataSourceConfiguration
{
  public void Configure(NpgsqlDataSourceBuilder builder)
  {
    builder.MapEnum<TariffEntity>();
  }
}
