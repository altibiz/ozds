using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;
using IEvent = Ozds.Business.Models.Abstractions.IEvent;

namespace Ozds.Business.Models.Base;

public abstract class EventModel : IdentifiableModel, IEvent
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
    foreach (var validationResult in base.Validate(validationContext))
    {
      yield return validationResult;
    }

    if (validationContext.ObjectInstance != this)
    {
      yield break;
    }

    var clock = validationContext.GetRequiredService<ClockQueries>();

    var now = clock.Timestamp();

    if (
      validationContext.MemberName is null or nameof(Timestamp) &&
      Timestamp > now
    )
    {
      yield return new ValidationResult(
        "Timestamp must be in the past",
        new[] { nameof(Timestamp) });
    }
  }
}
