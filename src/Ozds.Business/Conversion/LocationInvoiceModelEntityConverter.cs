using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class LocationInvoiceModelEntityConverter : ModelEntityConverter<
  LocationInvoiceModel, LocationInvoiceEntity>
{
  protected override LocationInvoiceEntity ToEntity(LocationInvoiceModel model)
  {
    return model.ToEntity();
  }

  protected override LocationInvoiceModel ToModel(LocationInvoiceEntity entity)
  {
    return entity.ToModel();
  }
}

public static class LocationInvoiceModelEntityConverterExtensions
{
  public static LocationInvoiceModel ToModel(this LocationInvoiceEntity entity)
  {
    return new LocationInvoiceModel
    {
      Id = entity.Id,
      Title = entity.Title,
      IssuedOn = entity.IssuedOn,
      IssuedById = entity.IssuedById,
      FromDate = entity.FromDate,
      ToDate = entity.ToDate,
      LocationId = entity.LocationId
    };
  }

  public static LocationInvoiceEntity ToEntity(this LocationInvoiceModel model)
  {
    return new LocationInvoiceEntity
    {
      Id = model.Id,
      Title = model.Title,
      IssuedOn = model.IssuedOn,
      IssuedById = model.IssuedById,
      FromDate = model.FromDate,
      ToDate = model.ToDate,
      LocationId = model.LocationId
    };
  }
}
