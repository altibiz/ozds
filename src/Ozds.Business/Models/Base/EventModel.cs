using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Ozds.Business.Models.Enums;
using IEvent = Ozds.Business.Models.Abstractions.IEvent;

namespace Ozds.Business.Models.Base;

public class EventModel : IdentifiableModel, IEvent
{
  [Required]
  public required List<CategoryModel> Categories { get; set; }

  [Required]
  public required DateTimeOffset Timestamp { get; set; }

  [Required]
  public required LevelModel Level { get; set; }

  [Required]
  public required JsonDocument Content { get; set; }

  public override IEnumerable<ValidationResult> Validate(
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
