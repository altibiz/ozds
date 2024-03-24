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
  public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);

  public abstract object ToDbEntity();
}
