using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Base;

// TODO: settings for the messenger

namespace Ozds.Data.Entities;

public class MessengerEntity : AuditableEntity
{
  [ForeignKey(nameof(Location))]
  public string LocationId { get; set; } = default!;

  public virtual LocationEntity Location { get; set; } = default!;

  public virtual ICollection<MeterEntity> Meters { get; set; } = default!;

  public virtual ICollection<MessengerEventEntity> Events { get; set; } = default!;
}
