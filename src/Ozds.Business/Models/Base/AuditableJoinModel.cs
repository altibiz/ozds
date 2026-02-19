using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;

namespace Ozds.Business.Models.Base;

public abstract class AuditableJoinModel : JoinModel, IAuditable
{
  public const string AuditingIdSeparator = ":";

  public string AuditingId
  {
    get { return LeftId + AuditingIdSeparator + RightId; }
  }

  public string AuditingTitle
  {
    get { return $"{GetType().Name} {AuditingId}"; }
  }

  [Required]
  public required DateTimeOffset CreatedOn { get; set; } =
    // NOTE: just so something is there
    DateTimeOffset.Parse("2000-01-01T00:00:00Z", CultureInfo.InvariantCulture);

  public required string? CreatedById { get; set; }

  public override IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext
  )
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
      validationContext.MemberName is null or nameof(CreatedOn)
      && CreatedOn > now
    )
    {
      yield return new ValidationResult(
        "Created on must be in the past",
        new[] { nameof(CreatedOn) }
      );
    }
  }
}
