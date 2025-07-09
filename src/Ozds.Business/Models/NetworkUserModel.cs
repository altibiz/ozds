using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Validation;

namespace Ozds.Business.Models;

public class NetworkUserModel : AuditableModel
{
  [Required]
  public required string LocationId { get; set; }

  [Required]
  public required LegalPersonModel LegalPerson { get; set; } = default!;

  [Required]
  public required string AltiBizSubProjectCode { get; set; } = default!;

  public required string InvoiceRemark { get; set; } = string.Empty;

  [Required]
  public required bool AutomaticallyApproveInvoices { get; set; } = false;

  public override IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    if (
      validationContext.MemberName is null or nameof(LegalPerson)
    )
    {
      foreach (var result in LegalPerson.Validate(validationContext))
      {
        yield return result;
      }
    }

    var sanitizer = validationContext.GetRequiredService<HtmlSanitizer>();
    var validationResult = sanitizer.Validate(InvoiceRemark);
    if (validationResult is not null)
    {
      yield return validationResult;
    }
  }
}
