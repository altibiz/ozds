using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract record InvoiceModel(
  string Id,
  DateTimeOffset IssuedOn,
  string? IssuedById,
  DateTimeOffset FromDate,
  DateTimeOffset ToDate
) : IInvoice
{
  public virtual IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    return Enumerable.Empty<ValidationResult>();
  }
}
