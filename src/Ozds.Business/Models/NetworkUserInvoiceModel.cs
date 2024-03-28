using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class NetworkUserInvoiceModel : InvoiceModel
{
  [Required] public required string NetworkUserId { get; set; }
}

public static class NetworkUserInvoiceModelExtensions
{
  public static NetworkUserInvoiceModel ToModel(
    this NetworkUserInvoiceEntity entity)
  {
    return new NetworkUserInvoiceModel
    {
      Id = entity.Id,
      IssuedOn = entity.IssuedOn,
      IssuedById = entity.IssuedById,
      FromDate = entity.FromDate,
      ToDate = entity.ToDate,
      NetworkUserId = entity.NetworkUserId
    };
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
