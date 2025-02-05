namespace Ozds.Document.Entities;

public abstract class
  ActiveEnergyTotalImportCalculationItemEntity : CalculationItemEntity
{
  public decimal Min_kWh { get; set; }

  public decimal Max_kWh { get; set; }

  public decimal Amount_kWh { get; set; }

  public override decimal Total
  {
    get { return Total_EUR; }
  }
}

public abstract class ActiveEnergyTotalImportT0CalculationItemEntity
  : ActiveEnergyTotalImportCalculationItemEntity
{
}

public abstract class ActiveEnergyTotalImportT1CalculationItemEntity
  : ActiveEnergyTotalImportCalculationItemEntity
{
}

public abstract class ActiveEnergyTotalImportT2CalculationItemEntity
  : ActiveEnergyTotalImportCalculationItemEntity
{
}

public class UsageActiveEnergyTotalImportT0CalculationItemEntity
  : ActiveEnergyTotalImportT0CalculationItemEntity
{
}

public class UsageActiveEnergyTotalImportT1CalculationItemEntity
  : ActiveEnergyTotalImportT1CalculationItemEntity
{
}

public class UsageActiveEnergyTotalImportT2CalculationItemEntity
  : ActiveEnergyTotalImportT2CalculationItemEntity
{
}

public class SupplyActiveEnergyTotalImportT1CalculationItemEntity
  : ActiveEnergyTotalImportT2CalculationItemEntity
{
}

public class SupplyActiveEnergyTotalImportT2CalculationItemEntity
  : ActiveEnergyTotalImportT2CalculationItemEntity
{
}

public class
  SupplyBusinessUsageCalculationItemEntity :
  ActiveEnergyTotalImportT0CalculationItemEntity
{
}

public class
  SupplyRenewableEnergyCalculationItemEntity :
  ActiveEnergyTotalImportT0CalculationItemEntity
{
}
