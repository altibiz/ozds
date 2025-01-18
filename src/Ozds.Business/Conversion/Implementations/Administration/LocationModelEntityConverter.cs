using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Administration;

public class LocationModelEntityConverter(IServiceProvider serviceProvider)
  : InheritingModelEntityConverter<
      LocationModel,
      IAuditable,
      LocationEntity,
      IAuditableEntity>(serviceProvider)
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    LocationModel model,
    LocationEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.WhiteMediumNetworkUserCatalogueId =
      model.WhiteMediumNetworkUserCatalogueId;
    entity.BlueLowNetworkUserCatalogueId =
      model.BlueLowNetworkUserCatalogueId;
    entity.WhiteLowNetworkUserCatalogueId =
      model.WhiteLowNetworkUserCatalogueId;
    entity.RedLowNetworkUserCatalogueId =
      model.RedLowNetworkUserCatalogueId;
    entity.RegulatoryCatalogueId = model.RegulatoryCatalogueId;
    entity.LegalPerson =
      entity.LegalPerson is null
        ? null!
        : modelEntityConverter
            .ToEntity<LegalPersonEntity>(model.LegalPerson);
    entity.AltiBizSubProjectCode = model.AltiBizSubProjectCode;
  }

  public override void InitializeModel(
    LocationEntity entity,
    LocationModel model)
  {
    base.InitializeModel(entity, model);
    model.WhiteMediumNetworkUserCatalogueId =
      entity.WhiteMediumNetworkUserCatalogueId;
    model.BlueLowNetworkUserCatalogueId =
      entity.BlueLowNetworkUserCatalogueId;
    model.WhiteLowNetworkUserCatalogueId =
      entity.WhiteLowNetworkUserCatalogueId;
    model.RedLowNetworkUserCatalogueId =
      entity.RedLowNetworkUserCatalogueId;
    model.RegulatoryCatalogueId = entity.RegulatoryCatalogueId;
    model.LegalPerson = entity.LegalPerson is null
      ? null!
      : modelEntityConverter
          .ToModel<LegalPersonModel>(entity.LegalPerson);
    model.AltiBizSubProjectCode = entity.AltiBizSubProjectCode;
  }
}
