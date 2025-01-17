using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Implementations.Complex;

public class ActiveEnergyTotalImportCalculationItemModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      ActiveEnergyTotalImportCalculationItemModel,
      CalculationItemModel>(serviceProvider)
{
  public override void Initialize(
    ActiveEnergyTotalImportCalculationItemModel model
  )
  {
    base.Initialize(model);

    model.Min_kWh = 0;
    model.Max_kWh = 0;
    model.Amount_kWh = 0;
  }
}

public class UsageActiveEnergyTotalImportT0CalculationItemModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      UsageActiveEnergyTotalImportT0CalculationItemModel,
      ActiveEnergyTotalImportCalculationItemModel>(serviceProvider)
{
}

public class UsageActiveEnergyTotalImportT1CalculationItemModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      UsageActiveEnergyTotalImportT1CalculationItemModel,
      ActiveEnergyTotalImportCalculationItemModel>(serviceProvider)
{
}

public class UsageActiveEnergyTotalImportT2CalculationItemModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      UsageActiveEnergyTotalImportT2CalculationItemModel,
      ActiveEnergyTotalImportCalculationItemModel>(serviceProvider)
{
}

public class SupplyActiveEnergyTotalImportT1CalculationItemModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      SupplyActiveEnergyTotalImportT1CalculationItemModel,
      ActiveEnergyTotalImportCalculationItemModel>(serviceProvider)
{
}

public class SupplyActiveEnergyTotalImportT2CalculationItemModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      SupplyActiveEnergyTotalImportT2CalculationItemModel,
      ActiveEnergyTotalImportCalculationItemModel>(serviceProvider)
{
}

public class SupplyBusinessUsageCalculationItemModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      SupplyBusinessUsageCalculationItemModel,
      ActiveEnergyTotalImportCalculationItemModel>(serviceProvider)
{
}

public class SupplyRenewableEnergyCalculationItemModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      SupplyRenewableEnergyCalculationItemModel,
      ActiveEnergyTotalImportCalculationItemModel>(serviceProvider)
{
}
