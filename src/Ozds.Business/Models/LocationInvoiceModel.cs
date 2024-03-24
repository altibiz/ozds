using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record LocationInvoiceModel(
  string Id,
  DateTimeOffset Timestamp,
  DateTimeOffset FromDate,
  DateTimeOffset ToDate,
  string LocationId
) : InvoiceModel(
  Id: Id,
  Timestamp: Timestamp,
  FromDate: FromDate,
  ToDate: ToDate
)
{
  public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
  {
    throw new NotImplementedException();
  }
}

public static class LocationInvoiceModelExtensions
{
  public static LocationInvoiceModel ToModel(this LocationInvoiceEntity entity) =>
    new(
      entity.Id,
      entity.Timestamp,
      entity.FromDate,
      entity.ToDate,
      entity.LocationId
    );

  public static LocationInvoiceEntity ToEntity(this LocationInvoiceModel model) =>
    new()
    {
      Id = model.Id,
      Timestamp = model.Timestamp,
      FromDate = model.FromDate,
      ToDate = model.ToDate,
      LocationId = model.LocationId
    };
}
