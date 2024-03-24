using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class RepresentativeEventEntity : EventEntity
{
  [ForeignKey(nameof(Representative))]
  public string RepresentativeId { get; set; } = default!;

  public virtual RepresentativeEntity Representative { get; set; } = default!;
}
