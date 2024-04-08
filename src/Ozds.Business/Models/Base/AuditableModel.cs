using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class AuditableModel : IdentifiableModel, IAuditable
{
  [Required]
  public required DateTimeOffset CreatedOn { get; set; } =
    DateTimeOffset.UtcNow;

  public required string? CreatedById { get; set; }
  public required DateTimeOffset? LastUpdatedOn { get; set; }
  public required string? LastUpdatedById { get; set; }
  public required bool IsDeleted { get; set; }
  public required DateTimeOffset? DeletedOn { get; set; }
  public required string? DeletedById { get; set; }

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

    if (
      validationContext.MemberName is null or nameof(IsDeleted)
        or nameof(DeletedOn) &&
      IsDeleted && !DeletedOn.HasValue
    )
    {
      yield return new ValidationResult(
        "Deleted on must be set if is deleted is true",
        new[] { nameof(IsDeleted), nameof(DeletedOn) });
    }

    if (
      validationContext.MemberName is null or nameof(LastUpdatedOn)
        or nameof(DeletedOn) &&
      LastUpdatedOn > DeletedOn
    )
    {
      yield return new ValidationResult(
        "Last updated on must be before deleted on",
        new[] { nameof(LastUpdatedOn), nameof(DeletedOn) });
    }

    if (
      validationContext.MemberName is null or nameof(CreatedOn)
        or nameof(LastUpdatedOn) &&
      CreatedOn > LastUpdatedOn
    )
    {
      yield return new ValidationResult(
        "Created on must be before last updated on",
        new[] { nameof(CreatedOn), nameof(LastUpdatedOn) });
    }

    if (
      validationContext.MemberName is null or nameof(CreatedOn) &&
      CreatedOn > DateTimeOffset.UtcNow
    )
    {
      yield return new ValidationResult(
        "Created on must be in the past",
        new[] { nameof(CreatedOn) });
    }

    if (
      validationContext.MemberName is null or nameof(LastUpdatedOn) &&
      LastUpdatedOn > DateTimeOffset.UtcNow
    )
    {
      yield return new ValidationResult(
        "Created on must be in the past",
        new[] { nameof(LastUpdatedOn) });
    }

    if (
      validationContext.MemberName is null or nameof(DeletedOn) &&
      DeletedOn > DateTimeOffset.UtcNow
    )
    {
      yield return new ValidationResult(
        "Created on must be in the past",
        new[] { nameof(DeletedOn) });
    }
  }
}
