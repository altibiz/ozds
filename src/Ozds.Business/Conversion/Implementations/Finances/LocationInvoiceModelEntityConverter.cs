using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class LocationInvoiceModelEntityConverter : ConcreteModelEntityConverter<
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
      LocationId = entity.LocationId,
      ArchivedLocation = entity.ArchivedLocation.ToModel(),
      Total_EUR = entity.Total_EUR,
      TaxRate_Percent = entity.TaxRate_Percent,
      Tax_EUR = entity.Tax_EUR,
      TotalWithTax_EUR = entity.TotalWithTax_EUR
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
      LocationId = model.LocationId,
      ArchivedLocation = model.ArchivedLocation.ToEntity(),
      Total_EUR = model.Total_EUR,
      TaxRate_Percent = model.TaxRate_Percent,
      Tax_EUR = model.Tax_EUR,
      TotalWithTax_EUR = model.TotalWithTax_EUR
    };
  }
}
