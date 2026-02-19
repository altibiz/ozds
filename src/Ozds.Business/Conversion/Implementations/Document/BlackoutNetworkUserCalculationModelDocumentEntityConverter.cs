using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class BlackoutNetworkUserCalculationModelEntityConverter(
  IServiceProvider serviceProvider
)
  : InheritingModelDocumentEntityConverter<
    BlackoutNetworkUserCalculationModel,
    NetworkUserCalculationModel,
    BlackoutNetworkUserCalculationEntity,
    NetworkUserCalculationEntity
  >(serviceProvider)
{
  private readonly ModelDocumentEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelDocumentEntityConverter>();

  public override void InitializeEntity(
    BlackoutNetworkUserCalculationModel model,
    BlackoutNetworkUserCalculationEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.ConcreteUsageNetworkUserCatalogue =
      modelEntityConverter.ToEntity<NetworkUserCatalogueEntity>(
        model.ConcreteArchivedUsageNetworkUserCatalogue
      );
    entity.Total_EUR = model.Total_EUR;
  }
}
