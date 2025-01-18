using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class ActiveEnergyTotalImportCalculationItemModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      ActiveEnergyTotalImportCalculationItemModel,
      CalculationItemModel,
      ActiveEnergyTotalImportCalculationItemEntity,
      CalculationItemEntity>(serviceProvider)
{
  public override void InitializeEntity(
    ActiveEnergyTotalImportCalculationItemModel model,
    ActiveEnergyTotalImportCalculationItemEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Min_kWh = model.Min_kWh;
    entity.Max_kWh = model.Max_kWh;
    entity.Amount_kWh = model.Amount_kWh;
  }

  public override void InitializeModel(
    ActiveEnergyTotalImportCalculationItemEntity entity,
    ActiveEnergyTotalImportCalculationItemModel model
  )
  {
    base.InitializeModel(entity, model);
    model.Min_kWh = entity.Min_kWh;
    model.Max_kWh = entity.Max_kWh;
    model.Amount_kWh = entity.Amount_kWh;
  }
}

public class UsageActiveEnergyTotalImportT0CalculationItemModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      UsageActiveEnergyTotalImportT0CalculationItemModel,
      ActiveEnergyTotalImportCalculationItemModel,
      UsageActiveEnergyTotalImportT0CalculationItemEntity,
      ActiveEnergyTotalImportCalculationItemEntity>(serviceProvider)
{
}

public class UsageActiveEnergyTotalImportT1CalculationItemModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      UsageActiveEnergyTotalImportT1CalculationItemModel,
      ActiveEnergyTotalImportCalculationItemModel,
      UsageActiveEnergyTotalImportT1CalculationItemEntity,
      ActiveEnergyTotalImportCalculationItemEntity>(serviceProvider)
{
}

public class UsageActiveEnergyTotalImportT2CalculationItemModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      UsageActiveEnergyTotalImportT2CalculationItemModel,
      ActiveEnergyTotalImportCalculationItemModel,
      UsageActiveEnergyTotalImportT2CalculationItemEntity,
      ActiveEnergyTotalImportCalculationItemEntity>(serviceProvider)
{
}

public class SupplyActiveEnergyTotalImportT1CalculationItemModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      SupplyActiveEnergyTotalImportT1CalculationItemModel,
      ActiveEnergyTotalImportCalculationItemModel,
      SupplyActiveEnergyTotalImportT1CalculationItemEntity,
      ActiveEnergyTotalImportCalculationItemEntity>(serviceProvider)
{
}

public class SupplyActiveEnergyTotalImportT2CalculationItemModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      SupplyActiveEnergyTotalImportT2CalculationItemModel,
      ActiveEnergyTotalImportCalculationItemModel,
      SupplyActiveEnergyTotalImportT2CalculationItemEntity,
      ActiveEnergyTotalImportCalculationItemEntity>(serviceProvider)
{
}

public class SupplyBusinessUsageCalculationItemModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      SupplyBusinessUsageCalculationItemModel,
      ActiveEnergyTotalImportCalculationItemModel,
      SupplyBusinessUsageFeeCalculationItemEntity,
      ActiveEnergyTotalImportCalculationItemEntity>(serviceProvider)
{
}

public class SupplyRenewableEnergyCalculationItemModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      SupplyRenewableEnergyCalculationItemModel,
      ActiveEnergyTotalImportCalculationItemModel,
      SupplyRenewableEnergyFeeCalculationItemEntity,
      ActiveEnergyTotalImportCalculationItemEntity>(serviceProvider)
{
}
