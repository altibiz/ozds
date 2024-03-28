using Microsoft.EntityFrameworkCore;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Enums;

public enum IntervalEntity
{
  QuarterHour,
  Day,
  Month
}

public class IntervalEntityTypeConfiguration : IModelConfiguration
{
  public void Configure(ModelBuilder modelBuilder)
  {
    modelBuilder.HasPostgresEnum<IntervalEntity>();
  }
}
