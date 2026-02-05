using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class MeteredNetworkUserCalculationModelDocumentEntityConverter(
  IServiceProvider serviceProvider
)
  : InheritingModelDocumentEntityConverter<
    MeteredNetworkUserCalculationModel,
    NetworkUserCalculationModel,
    MeteredNetworkUserCalculationEntity,
    NetworkUserCalculationEntity
  >(serviceProvider)
{
  private readonly ModelDocumentEntityConverter modelDocumentEntityConverter =
    serviceProvider.GetRequiredService<ModelDocumentEntityConverter>();

  public override void InitializeEntity(
    MeteredNetworkUserCalculationModel model,
    MeteredNetworkUserCalculationEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.UsageMeterFee =
      modelDocumentEntityConverter.ToEntity<UsageMeterFeeCalculationItemEntity>(
        model.UsageMeterFee
      );
    entity.SupplyActiveEnergyTotalImportT1 =
      modelDocumentEntityConverter.ToEntity<SupplyActiveEnergyTotalImportT1CalculationItemEntity>(
        model.SupplyActiveEnergyTotalImportT1
      );
    entity.SupplyActiveEnergyTotalImportT2 =
      modelDocumentEntityConverter.ToEntity<SupplyActiveEnergyTotalImportT2CalculationItemEntity>(
        model.SupplyActiveEnergyTotalImportT2
      );
    entity.SupplyBusinessUsageFee =
      modelDocumentEntityConverter.ToEntity<SupplyBusinessUsageCalculationItemEntity>(
        model.SupplyBusinessUsageFee
      );
    entity.SupplyRenewableEnergyFee =
      modelDocumentEntityConverter.ToEntity<SupplyRenewableEnergyCalculationItemEntity>(
        model.SupplyRenewableEnergyFee
      );
    entity.UsageFeeTotal_EUR = model.UsageFeeTotal_EUR;
    entity.SupplyFeeTotal_EUR = model.SupplyFeeTotal_EUR;
  }
}
