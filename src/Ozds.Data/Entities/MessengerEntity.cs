using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class MessengerEntity : AuditableEntity
{
  public virtual LocationEntity Location { get; set; } = default!;

  public virtual ICollection<MeterEntity> Meters { get; set; } = default!;

  public virtual ICollection<MessengerEventEntity> Events { get; set; } = default!;
}
