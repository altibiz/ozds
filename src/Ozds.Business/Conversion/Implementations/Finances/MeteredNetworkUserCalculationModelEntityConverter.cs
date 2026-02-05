using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class MeteredNetworkUserCalculationModelEntityConverter(
  IServiceProvider serviceProvider
)
  : InheritingModelEntityConverter<
    MeteredNetworkUserCalculationModel,
    NetworkUserCalculationModel,
    MeteredNetworkUserCalculationEntity,
    NetworkUserCalculationEntity
  >(serviceProvider)
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    MeteredNetworkUserCalculationModel model,
    MeteredNetworkUserCalculationEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.UsageMeterFee = model.UsageMeterFee is null
      ? null!
      : modelEntityConverter.ToEntity<UsageMeterFeeCalculationItemEntity>(
        model.UsageMeterFee
      );
    entity.SupplyActiveEnergyTotalImportT1 =
      model.SupplyActiveEnergyTotalImportT1 is null
        ? null!
        : modelEntityConverter.ToEntity<SupplyActiveEnergyTotalImportT1CalculationItemEntity>(
          model.SupplyActiveEnergyTotalImportT1
        );
    entity.SupplyActiveEnergyTotalImportT2 =
      model.SupplyActiveEnergyTotalImportT2 is null
        ? null!
        : modelEntityConverter.ToEntity<SupplyActiveEnergyTotalImportT2CalculationItemEntity>(
          model.SupplyActiveEnergyTotalImportT2
        );
    entity.SupplyBusinessUsageFee = model.SupplyBusinessUsageFee is null
      ? null!
      : modelEntityConverter.ToEntity<SupplyBusinessUsageCalculationItemEntity>(
        model.SupplyBusinessUsageFee
      );
    entity.SupplyRenewableEnergyFee = model.SupplyRenewableEnergyFee is null
      ? null!
      : modelEntityConverter.ToEntity<SupplyRenewableEnergyCalculationItemEntity>(
        model.SupplyRenewableEnergyFee
      );

    entity.UsageFeeTotal_EUR = model.UsageFeeTotal_EUR;
    entity.SupplyFeeTotal_EUR = model.SupplyFeeTotal_EUR;
    entity.Total_EUR = model.Total_EUR;
  }

  public override void InitializeModel(
    MeteredNetworkUserCalculationEntity entity,
    MeteredNetworkUserCalculationModel model
  )
  {
    base.InitializeModel(entity, model);
    model.UsageMeterFee = entity.UsageMeterFee is null
      ? null!
      : modelEntityConverter.ToModel<UsageMeterFeeCalculationItemModel>(
        entity.UsageMeterFee
      );
    model.SupplyActiveEnergyTotalImportT1 =
      entity.SupplyActiveEnergyTotalImportT1 is null
        ? null!
        : modelEntityConverter.ToModel<SupplyActiveEnergyTotalImportT1CalculationItemModel>(
          entity.SupplyActiveEnergyTotalImportT1
        );
    model.SupplyActiveEnergyTotalImportT2 =
      entity.SupplyActiveEnergyTotalImportT2 is null
        ? null!
        : modelEntityConverter.ToModel<SupplyActiveEnergyTotalImportT2CalculationItemModel>(
          entity.SupplyActiveEnergyTotalImportT2
        );
    model.SupplyBusinessUsageFee = entity.SupplyBusinessUsageFee is null
      ? null!
      : modelEntityConverter.ToModel<SupplyBusinessUsageCalculationItemModel>(
        entity.SupplyBusinessUsageFee
      );
    model.SupplyRenewableEnergyFee = entity.SupplyRenewableEnergyFee is null
      ? null!
      : modelEntityConverter.ToModel<SupplyRenewableEnergyCalculationItemModel>(
        entity.SupplyRenewableEnergyFee
      );

    model.UsageFeeTotal_EUR = entity.UsageFeeTotal_EUR;
    model.SupplyFeeTotal_EUR = entity.SupplyFeeTotal_EUR;
    model.Total_EUR = entity.Total_EUR;
  }
}
