using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class MeterEventEntity : EventEntity
{
  public virtual MeterEntity Meter { get; set; } = default!;
}
