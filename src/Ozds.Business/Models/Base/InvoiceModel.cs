using System.ComponentModel.DataAnnotations;

namespace Ozds.Business.Models.Base;

public abstract record InvoiceModel(
  string Id,
  DateTimeOffset Timestamp,
  DateTimeOffset FromDate,
  DateTimeOffset ToDate
) : IValidatableObject
{
  public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
}
