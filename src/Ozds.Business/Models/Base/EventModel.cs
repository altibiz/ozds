using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Base;

public abstract class EventModel : IEvent
{
  [Required] public required string Id { get; init; }

  [Required] public required string Title { get; init; }

  [Required] public required DateTimeOffset Timestamp { get; init; }

  [Required] public required LevelModel Level { get; init; }

  [Required] public required string Description { get; init; }

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
