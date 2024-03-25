using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record NetworkUserInvoiceModel(
  string Id,
  DateTimeOffset IssuedOn,
  string? IssuedById,
  DateTimeOffset FromDate,
  DateTimeOffset ToDate,
  string NetworkUserId
) : InvoiceModel(
  Id,
  IssuedOn,
  IssuedById,
  FromDate,
  ToDate
)
{
  public override IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    throw new NotImplementedException();
  }
}

public static class NetworkUserInvoiceModelExtensions
{
  public static NetworkUserInvoiceModel ToModel(
    this NetworkUserInvoiceEntity entity)
  {
    return new NetworkUserInvoiceModel(
      entity.Id,
      entity.IssuedOn,
      entity.IssuedById,
      entity.FromDate,
      entity.ToDate,
      entity.NetworkUserId
    );
  }

  public static NetworkUserInvoiceEntity ToEntity(
    this NetworkUserInvoiceModel model)
  {
    return new NetworkUserInvoiceEntity
    {
      Id = model.Id,
      IssuedOn = model.IssuedOn,
      IssuedById = model.IssuedById,
      FromDate = model.FromDate,
      ToDate = model.ToDate,
      NetworkUserId = model.NetworkUserId
    };
  }
}
