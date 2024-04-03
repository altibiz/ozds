using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class NetworkUserInvoiceModelEntityConverter : ModelEntityConverter<NetworkUserInvoiceModel, NetworkUserInvoiceEntity>
{
  protected override NetworkUserInvoiceEntity ToEntity(NetworkUserInvoiceModel model) =>
    model.ToEntity();

  protected override NetworkUserInvoiceModel ToModel(NetworkUserInvoiceEntity entity) =>
    entity.ToModel();
}


public static class NetworkUserInvoiceModelEntityConverterExtensions
{
  public static NetworkUserInvoiceModel ToModel(
    this NetworkUserInvoiceEntity entity)
  {
    return new NetworkUserInvoiceModel
    {
      Id = entity.Id,
      Title = entity.Title,
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
      Title = model.Title,
      IssuedOn = model.IssuedOn,
      IssuedById = model.IssuedById,
      FromDate = model.FromDate,
      ToDate = model.ToDate,
      NetworkUserId = model.NetworkUserId
    };
  }
}
