using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record RegulatoryCatalogueModel(
  string Id,
  string Title,
  DateTimeOffset CreatedOn,
  string? CreatedById,
  DateTimeOffset? LastUpdatedOn,
  string? LastUpdatedById,
  bool IsDeleted,
  DateTimeOffset? DeletedOn,
  string? DeletedById,
  string LocationId,
  float ActiveEnergyTotalImportT1Price_EUR,
  float ActiveEnergyTotalImportT2Price_EUR,
  float RenewableEnergyFeePrice_EUR,
  float BusinessUsageFeePrice_EUR,
  float TaxRate_Percent
) : CatalogueModel(
  Id: Id,
  Title: Title,
  CreatedOn: CreatedOn,
  CreatedById: CreatedById,
  LastUpdatedOn: LastUpdatedOn,
  LastUpdatedById: LastUpdatedById,
  IsDeleted: IsDeleted,
  DeletedOn: DeletedOn,
  DeletedById: DeletedById,
  LocationId: LocationId
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

public static class RegulatoryCatalogueModelExtensions
{
  public static RegulatoryCatalogueModel ToModel(this RegulatoryCatalogueEntity entity) =>
    new(
      entity.Id,
      entity.Title,
      entity.CreatedOn,
      entity.CreatedById,
      entity.LastUpdatedOn,
      entity.LastUpdatedById,
      entity.IsDeleted,
      entity.DeletedOn,
      entity.DeletedById,
      entity.LocationId,
      entity.ActiveEnergyTotalImportT1Price_EUR,
      entity.ActiveEnergyTotalImportT2Price_EUR,
      entity.RenewableEnergyFeePrice_EUR,
      entity.BusinessUsageFeePrice_EUR,
      entity.TaxRate_Percent
    );

  public static RegulatoryCatalogueEntity ToEntity(this RegulatoryCatalogueModel model) =>
    new()
    {
      Id = model.Id,
      Title = model.Title,
      CreatedOn = model.CreatedOn,
      CreatedById = model.CreatedById,
      LastUpdatedOn = model.LastUpdatedOn,
      LastUpdatedById = model.LastUpdatedById,
      IsDeleted = model.IsDeleted,
      DeletedOn = model.DeletedOn,
      DeletedById = model.DeletedById,
      LocationId = model.LocationId,
      ActiveEnergyTotalImportT1Price_EUR = model.ActiveEnergyTotalImportT1Price_EUR,
      ActiveEnergyTotalImportT2Price_EUR = model.ActiveEnergyTotalImportT2Price_EUR,
      RenewableEnergyFeePrice_EUR = model.RenewableEnergyFeePrice_EUR,
      BusinessUsageFeePrice_EUR = model.BusinessUsageFeePrice_EUR,
      TaxRate_Percent = model.TaxRate_Percent
    };
}
