using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;

namespace Ozds.Business.Models.Base;

public abstract class FinancialModel : IdentifiableModel, IFinancial
{
  public required string Remark { get; set; } = string.Empty;

  [Required]
  public required DateTimeOffset IssuedOn { get; set; }

  [Required]
  public required string? IssuedById { get; set; }

  [Required]
  public required DateTimeOffset FromDate { get; set; }

  [Required]
  public required DateTimeOffset ToDate { get; set; }

  [Required]
  public required decimal Total_EUR { get; set; }

  public abstract decimal TaxRate_Percent { get; }

  public abstract decimal Tax_EUR { get; }

  public abstract decimal TotalWithTax_EUR { get; }

  public override IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    if (validationContext.ObjectInstance != this)
    {
      yield break;
    }

    if (
      validationContext.MemberName is null or nameof(FromDate)
        or nameof(ToDate) &&
      FromDate > ToDate
    )
    {
      yield return new ValidationResult(
        "From date must be before to date",
        new[] { nameof(FromDate), nameof(ToDate) });
    }

    if (
      (validationContext.MemberName is null or nameof(IssuedOn)
          or nameof(FromDate) or nameof(ToDate) &&
        IssuedOn < FromDate) || IssuedOn < ToDate
    )
    {
      yield return new ValidationResult(
        "Issued on must be after from date and to date",
        new[] { nameof(IssuedOn), nameof(FromDate), nameof(ToDate) });
    }

    var clock = validationContext.GetRequiredService<ClockQueries>();

    var now = clock.Timestamp();

    if (
      validationContext.MemberName is null or nameof(IssuedOn) &&
      IssuedOn > now
    )
    {
      yield return new ValidationResult(
        "Issued on must be in the past",
        new[] { nameof(IssuedOn) });
    }

    if (
      validationContext.MemberName is null or nameof(FromDate) &&
      FromDate > now
    )
    {
      yield return new ValidationResult(
        "From date must be in the past",
        new[] { nameof(FromDate) });
    }

    if (
      validationContext.MemberName is null or nameof(ToDate) &&
      ToDate > now
    )
    {
      yield return new ValidationResult(
        "To date must be in the past",
        new[] { nameof(ToDate) });
    }
  }
}
