using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class MessengerEventEntity : EventEntity
{
  [ForeignKey(nameof(Messenger))]
  public string MessengerId { get; set; } = default!;

  public virtual MeterEntity Messenger { get; set; } = default!;
}
