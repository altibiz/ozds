using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class BlackoutNetworkUserCalculationModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
  BlackoutNetworkUserCalculationModel,
  NetworkUserCalculationModel,
  BlackoutNetworkUserCalculationEntity,
  NetworkUserCalculationEntity>(serviceProvider)
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    BlackoutNetworkUserCalculationModel model,
    BlackoutNetworkUserCalculationEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.ArchivedUsageNetworkUserCatalogue =
      modelEntityConverter.ToEntity<NetworkUserCatalogueEntity>(
        model.ConcreteArchivedUsageNetworkUserCatalogue);
    entity.Total_EUR = model.Total_EUR;
  }

  public override void InitializeModel(
    BlackoutNetworkUserCalculationEntity entity,
    BlackoutNetworkUserCalculationModel model
  )
  {
    base.InitializeModel(entity, model);
    model.ConcreteArchivedUsageNetworkUserCatalogue =
      modelEntityConverter.ToModel<NetworkUserCatalogueModel>(
        entity.ArchivedUsageNetworkUserCatalogue);
    model.Total_EUR = entity.Total_EUR;
  }
}
