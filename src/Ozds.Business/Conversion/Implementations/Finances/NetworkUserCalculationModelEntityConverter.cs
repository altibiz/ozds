using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class NetworkUserCalculationModelEntityConverter(
  IServiceProvider serviceProvider
)
  : InheritingModelEntityConverter<
    NetworkUserCalculationModel,
    CalculationModel,
    NetworkUserCalculationEntity,
    CalculationEntity
  >(serviceProvider)
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    NetworkUserCalculationModel model,
    NetworkUserCalculationEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.NetworkUserMeasurementLocationId =
      model.NetworkUserMeasurementLocationId;
    entity.ArchivedNetworkUserMeasurementLocation =
      model.ArchivedNetworkUserMeasurementLocation is null
        ? null!
        : modelEntityConverter.ToEntity<NetworkUserMeasurementLocationEntity>(
          model.ArchivedNetworkUserMeasurementLocation
        );
    if (
      entity.ArchivedNetworkUserMeasurementLocation is
      { } archivedNetworkUserMeasurementLocation
    )
    {
      archivedNetworkUserMeasurementLocation.Kind =
        archivedNetworkUserMeasurementLocation.GetType().Name;
    }

    entity.SupplyRegulatoryCatalogueId = model.SupplyRegulatoryCatalogueId;
    entity.ArchivedSupplyRegulatoryCatalogue =
      model.ArchivedSupplyRegulatoryCatalogue is null
        ? null!
        : modelEntityConverter.ToEntity<RegulatoryCatalogueEntity>(
          model.ArchivedSupplyRegulatoryCatalogue
        );
    entity.NetworkUserInvoiceId = model.NetworkUserInvoiceId;
    entity.MeterId = model.MeterId;
    entity.ArchivedMeter = model.ArchivedMeter is null
      ? null!
      : modelEntityConverter.ToEntity<MeterEntity>(model.ArchivedMeter);
    if (entity.ArchivedMeter is { } archivedMeter)
    {
      archivedMeter.Kind = archivedMeter.GetType().Name;
    }

    entity.Total_EUR = model.Total_EUR;
  }

  public override void InitializeModel(
    NetworkUserCalculationEntity entity,
    NetworkUserCalculationModel model
  )
  {
    base.InitializeModel(entity, model);
    model.NetworkUserMeasurementLocationId =
      entity.NetworkUserMeasurementLocationId;
    model.ArchivedNetworkUserMeasurementLocation =
      entity.ArchivedNetworkUserMeasurementLocation is null
        ? null!
        : modelEntityConverter.ToModel<NetworkUserMeasurementLocationModel>(
          entity.ArchivedNetworkUserMeasurementLocation
        );
    model.SupplyRegulatoryCatalogueId = entity.SupplyRegulatoryCatalogueId;
    model.ArchivedSupplyRegulatoryCatalogue =
      entity.ArchivedSupplyRegulatoryCatalogue is null
        ? null!
        : modelEntityConverter.ToModel<RegulatoryCatalogueModel>(
          entity.ArchivedSupplyRegulatoryCatalogue
        );
    model.NetworkUserInvoiceId = entity.NetworkUserInvoiceId;
    model.MeterId = entity.MeterId;
    model.ArchivedMeter = entity.ArchivedMeter is null
      ? null!
      : modelEntityConverter.ToModel<MeterModel>(entity.ArchivedMeter);
    model.Total_EUR = entity.Total_EUR;
  }
}
