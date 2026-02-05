using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Business.Queries;

namespace Ozds.Business.Models;

public class ApiKeyModel : TrackableModel
{
  [Required]
  public required string PrincipalModelType { get; set; } = default!;

  [Required]
  public required string PrincipalModelId { get; set; } = default!;

  // NOTE: only used during creation to display to user
  public string? Value { get; set; } = default!;

  [Required]
  public required string Hash { get; set; } = default!;

  public DateTimeOffset? ExpiresOn { get; set; } = default!;

  public override IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext
  )
  {
    foreach (var validationResult in base.Validate(validationContext))
    {
      yield return validationResult;
    }

    if (validationContext.MemberName is null or nameof(ExpiresOn))
    {
      var clockQueries = validationContext.GetRequiredService<ClockQueries>();
      var now = clockQueries.Now();

      if (ExpiresOn is { } expiresOn && expiresOn < now)
      {
        yield return new ValidationResult(
          "Expires on must be in the future",
          new[] { nameof(ExpiresOn) }
        );
      }
    }
  }
}
