using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class AuditableModel : IdentifiableModel, IAuditable
{
  [Required]
  public required DateTimeOffset CreatedOn { get; init; } = DateTimeOffset.UtcNow;
  public required string? CreatedById { get; init; }
  public required DateTimeOffset? LastUpdatedOn { get; init; }
  public required string? LastUpdatedById { get; init; }
  public required bool IsDeleted { get; init; }
  public required DateTimeOffset? DeletedOn { get; init; }
  public required string? DeletedById { get; init; }

  public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
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
      (validationContext.MemberName is null or nameof(IsDeleted) or nameof(DeletedOn)) &&
      IsDeleted && !DeletedOn.HasValue
    )
    {
      yield return new ValidationResult(
        "Deleted on must be set if is deleted is true",
        new[] { nameof(IsDeleted), nameof(DeletedOn) });
    }

    if (
      (validationContext.MemberName is null or nameof(LastUpdatedOn) or nameof(DeletedOn)) &&
      LastUpdatedOn > DeletedOn
    )
    {
      yield return new ValidationResult(
        "Last updated on must be before deleted on",
        new[] { nameof(LastUpdatedOn), nameof(DeletedOn) });
    }

    if (
      (validationContext.MemberName is null or nameof(CreatedOn) or nameof(LastUpdatedOn)) &&
      CreatedOn > LastUpdatedOn
    )
    {
      yield return new ValidationResult(
        "Created on must be before last updated on",
        new[] { nameof(CreatedOn), nameof(LastUpdatedOn) });
    }

    if (
      (validationContext.MemberName is null or nameof(CreatedOn)) &&
      CreatedOn > DateTimeOffset.UtcNow
    )
    {
      yield return new ValidationResult(
        "Created on must be in the past",
        new[] { nameof(CreatedOn) });
    }

    if (
      (validationContext.MemberName is null or nameof(LastUpdatedOn)) &&
      LastUpdatedOn > DateTimeOffset.UtcNow
    )
    {
      yield return new ValidationResult(
        "Created on must be in the past",
        new[] { nameof(LastUpdatedOn) });
    }

    if (
      (validationContext.MemberName is null or nameof(DeletedOn)) &&
      DeletedOn > DateTimeOffset.UtcNow
    )
    {
      yield return new ValidationResult(
        "Created on must be in the past",
        new[] { nameof(DeletedOn) });
    }
  }
}
