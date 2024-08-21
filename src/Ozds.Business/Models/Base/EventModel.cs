using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;
using Ozds.Business.Models.Enums;
using IEvent = Ozds.Business.Models.Abstractions.IEvent;

namespace Ozds.Business.Models.Base;

public abstract class EventModel : IEvent
{
  [Required]
  public required string Id { get; set; }

  [Required]
  public required string Title { get; set; }

  [Required]
  public required DateTimeOffset Timestamp { get; set; }

  [Required]
  public required LevelModel Level { get; set; }

  [Required]
  public required JsonObject Content { get; set; }

  public virtual IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    if (validationContext.ObjectInstance != this)
    {
      yield break;
    }

    if (
      validationContext.MemberName is null or nameof(Timestamp) &&
      Timestamp > DateTimeOffset.UtcNow
    )
    {
      yield return new ValidationResult(
        "Timestamp must be in the past",
        new[] { nameof(Timestamp) });
    }
  }
}
