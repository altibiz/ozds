using Ozds.Data.Attributes;

namespace Ozds.Data.Entities.Enums;

[PostgresqlEnum]
public enum IntervalEntity
{
  QuarterHour,
  Day,
  Month
}