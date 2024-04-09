using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class NetworkUserInvoiceModelEntityConverter : ModelEntityConverter<
  NetworkUserInvoiceModel, NetworkUserInvoiceEntity>
{
  protected override NetworkUserInvoiceEntity ToEntity(
    NetworkUserInvoiceModel model)
  {
    return model.ToEntity();
  }

  protected override NetworkUserInvoiceModel ToModel(
    NetworkUserInvoiceEntity entity)
  {
    return entity.ToModel();
  }
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
      NetworkUserId = entity.NetworkUserId,
      Total_EUR = entity.Total_EUR,
      Tax_EUR = entity.Tax_EUR,
      TotalWithTax_EUR = entity.TotalWithTax_EUR,
      ArchivedLocation = entity.ArchivedLocation.ToModel(),
      ArchivedNetworkUser = entity.ArchivedNetworkUser.ToModel()
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
      NetworkUserId = model.NetworkUserId,
      Total_EUR = model.Total_EUR,
      Tax_EUR = model.Tax_EUR,
      TotalWithTax_EUR = model.TotalWithTax_EUR,
      ArchivedLocation = model.ArchivedLocation.ToEntity(),
      ArchivedNetworkUser = model.ArchivedNetworkUser.ToEntity()
    };
  }
}
