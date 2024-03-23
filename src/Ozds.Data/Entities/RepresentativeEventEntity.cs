using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class RepresentativeEventEntity : EventEntity
{
  public virtual RepresentativeEntity Representative { get; set; } = default!;
}
