using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ozds.Data.Context;

namespace Ozds.Data.Entities.Enums;

public enum OrderOfMagnitudeEntity
{
  Giga = 9,
  Mega = 6,
  Kilo = 3,
  Hecto = 2,
  Deca = 1,
  Deci = -1,
  Centi = -2,
  Milli = -3,
  Micro = -6,
  Nano = -9,
}

public class OrderOfMagnitudeEntityTypeConfiguration : IModelConfiguration
{
  public void Configure(ModelBuilder modelBuilder)
  {
    modelBuilder.HasPostgresEnum<OrderOfMagnitudeEntity>();
  }
}

public class OrderOfMagnitudeEntityNpgsqlDataSourceConfiguration
  : INpgsqlDataSourceConfiguration
{
  public void Configure(NpgsqlDataSourceBuilder builder)
  {
    builder.MapEnum<OrderOfMagnitudeEntity>();
  }
}
