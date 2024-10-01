using Ozds.Data.Entities.Enums;

namespace Ozds.Data.Entities.Complex;

public class PeriodEntity
{
  public DurationEntity Duration { get; set; }

  public uint Multiplier { get; set; }
}
