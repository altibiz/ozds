using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class LocationInvoiceModel : InvoiceModel
{
  [Required] public required string LocationId { get; set; }
}

public static class LocationInvoiceModelExtensions
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
