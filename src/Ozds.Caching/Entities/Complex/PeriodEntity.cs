using Ozds.Caching.Entities.Base;
using Ozds.Caching.Entities.Enums;

namespace Ozds.Caching.Entities.Complex;

public class PeriodEntity : Entity
{
  public DurationEntity Duration { get; set; }

  public uint Multiplier { get; set; }
}
