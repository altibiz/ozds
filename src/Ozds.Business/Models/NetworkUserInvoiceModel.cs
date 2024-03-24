using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record NetworkUserInvoiceModel(
  string Id,
  DateTimeOffset Timestamp,
  DateTimeOffset FromDate,
  DateTimeOffset ToDate,
  string NetworkUserId
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

public static class NetworkUserInvoiceModelExtensions
{
  public static NetworkUserInvoiceModel ToModel(this NetworkUserInvoiceEntity entity) =>
    new(
      entity.Id,
      entity.Timestamp,
      entity.FromDate,
      entity.ToDate,
      entity.NetworkUserId
    );

  public static NetworkUserInvoiceEntity ToEntity(this NetworkUserInvoiceModel model) =>
    new()
    {
      Id = model.Id,
      Timestamp = model.Timestamp,
      FromDate = model.FromDate,
      ToDate = model.ToDate,
      NetworkUserId = model.NetworkUserId
    };
}
