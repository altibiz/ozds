using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ozds.Data.Context;

namespace Ozds.Data.Entities.Enums;

public enum MeasureEntity
{
  Current,
  Voltage,
  ActivePower,
  ReactivePower,
  ApparentPower,
  ActiveEnergy,
  ReactiveEnergy,
  ApparentEnergy
}

public class MeasureEntityTypeConfiguration : IModelConfiguration
{
  public void Configure(ModelBuilder modelBuilder)
  {
    modelBuilder.HasPostgresEnum<MeasureEntity>();
  }
}

public class
  MeasureEntityNpgsqlDataSourceConfiguration : INpgsqlDataSourceConfiguration
{
  public void Configure(NpgsqlDataSourceBuilder builder)
  {
    builder.MapEnum<MeasureEntity>();
  }
}
