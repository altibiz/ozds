using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ozds.Data.Context;

namespace Ozds.Data.Entities.Enums;

public enum AggregationEntity
{
  Min,
  Max,
  Avg,
}

public class AggregationEntityTypeConfiguration : IModelConfiguration
{
  public void Configure(ModelBuilder modelBuilder)
  {
    modelBuilder.HasPostgresEnum<AggregationEntity>();
  }
}

public class AggregationEntityNpgsqlDataSourceConfiguration
  : INpgsqlDataSourceConfiguration
{
  public void Configure(NpgsqlDataSourceBuilder builder)
  {
    builder.MapEnum<AggregationEntity>();
  }
}
