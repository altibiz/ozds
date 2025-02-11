using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class ActiveEnergyTotalImportCalculationItemModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelDocumentEntityConverter<
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
}

public class UsageActiveEnergyTotalImportT0CalculationItemModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelDocumentEntityConverter<
  UsageActiveEnergyTotalImportT0CalculationItemModel,
  ActiveEnergyTotalImportCalculationItemModel,
  UsageActiveEnergyTotalImportT0CalculationItemEntity,
  ActiveEnergyTotalImportCalculationItemEntity>(serviceProvider)
{
}

public class UsageActiveEnergyTotalImportT1CalculationItemModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelDocumentEntityConverter<
  UsageActiveEnergyTotalImportT1CalculationItemModel,
  ActiveEnergyTotalImportCalculationItemModel,
  UsageActiveEnergyTotalImportT1CalculationItemEntity,
  ActiveEnergyTotalImportCalculationItemEntity>(serviceProvider)
{
}

public class UsageActiveEnergyTotalImportT2CalculationItemModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelDocumentEntityConverter<
  UsageActiveEnergyTotalImportT2CalculationItemModel,
  ActiveEnergyTotalImportCalculationItemModel,
  UsageActiveEnergyTotalImportT2CalculationItemEntity,
  ActiveEnergyTotalImportCalculationItemEntity>(serviceProvider)
{
}

public class SupplyActiveEnergyTotalImportT1CalculationItemModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelDocumentEntityConverter<
  SupplyActiveEnergyTotalImportT1CalculationItemModel,
  ActiveEnergyTotalImportCalculationItemModel,
  SupplyActiveEnergyTotalImportT1CalculationItemEntity,
  ActiveEnergyTotalImportCalculationItemEntity>(serviceProvider)
{
}

public class SupplyActiveEnergyTotalImportT2CalculationItemModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelDocumentEntityConverter<
  SupplyActiveEnergyTotalImportT2CalculationItemModel,
  ActiveEnergyTotalImportCalculationItemModel,
  SupplyActiveEnergyTotalImportT2CalculationItemEntity,
  ActiveEnergyTotalImportCalculationItemEntity>(serviceProvider)
{
}

public class SupplyBusinessUsageCalculationItemModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelDocumentEntityConverter<
  SupplyBusinessUsageCalculationItemModel,
  ActiveEnergyTotalImportCalculationItemModel,
  SupplyBusinessUsageCalculationItemEntity,
  ActiveEnergyTotalImportCalculationItemEntity>(serviceProvider)
{
}

public class SupplyRenewableEnergyCalculationItemModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelDocumentEntityConverter<
  SupplyRenewableEnergyCalculationItemModel,
  ActiveEnergyTotalImportCalculationItemModel,
  SupplyRenewableEnergyCalculationItemEntity,
  ActiveEnergyTotalImportCalculationItemEntity>(serviceProvider)
{
}
