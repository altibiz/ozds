using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record LocationInvoiceModel(
  string Id,
  DateTimeOffset IssuedOn,
  string? IssuedById,
  DateTimeOffset FromDate,
  DateTimeOffset ToDate,
  string LocationId
) : InvoiceModel(
  Id: Id,
  IssuedOn: IssuedOn,
  IssuedById: IssuedById,
  FromDate: FromDate,
  ToDate: ToDate
)
{
  public override object ToDbEntity()
  {
    return this.ToEntity();
  }

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
      entity.IssuedOn,
      entity.IssuedById,
      entity.FromDate,
      entity.ToDate,
      entity.LocationId
    );

  public static LocationInvoiceEntity ToEntity(this LocationInvoiceModel model) =>
    new()
    {
      Id = model.Id,
      IssuedOn = model.IssuedOn,
      IssuedById = model.IssuedById,
      FromDate = model.FromDate,
      ToDate = model.ToDate,
      LocationId = model.LocationId
    };
}
