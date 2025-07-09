using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Business.Validation;

namespace Ozds.Business.Models;

public class NetworkUserMeasurementLocationModel : MeasurementLocationModel
{
  [Required]
  public required string NetworkUserId { get; set; }

  [Required]
  public required string NetworkUserCatalogueId { get; set; }

  public required string CalculationRemark { get; set; } = string.Empty;

  public override IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext
  )
  {
    var sanitizer = validationContext.GetRequiredService<HtmlSanitizer>();
    var validationResult = sanitizer.Validate(CalculationRemark);
    if (validationResult is not null)
    {
      yield return validationResult;
    }
  }
}
