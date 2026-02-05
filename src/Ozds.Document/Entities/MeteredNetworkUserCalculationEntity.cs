namespace Ozds.Document.Entities;

public abstract class MeteredNetworkUserCalculationEntity
  : NetworkUserCalculationEntity
{
  public UsageMeterFeeCalculationItemEntity UsageMeterFee { get; set; } =
    default!;

  public SupplyActiveEnergyTotalImportT1CalculationItemEntity SupplyActiveEnergyTotalImportT1 { get; set; } =
    default!;

  public SupplyActiveEnergyTotalImportT2CalculationItemEntity SupplyActiveEnergyTotalImportT2 { get; set; } =
    default!;

  public SupplyBusinessUsageCalculationItemEntity SupplyBusinessUsageFee { get; set; } =
    default!;

  public SupplyRenewableEnergyCalculationItemEntity SupplyRenewableEnergyFee { get; set; } =
    default!;

  public decimal UsageFeeTotal_EUR { get; set; }

  public decimal SupplyFeeTotal_EUR { get; set; }

  public virtual IEnumerable<CalculationItemEntity> UsageItems
  {
    get
    {
      return AdditionalUsageItems
        .AsEnumerable()
        .Concat(new CalculationItemEntity[] { UsageMeterFee });
    }
  }

  public virtual IEnumerable<CalculationItemEntity> SupplyItems
  {
    get
    {
      return new CalculationItemEntity[]
      {
        SupplyActiveEnergyTotalImportT1,
        SupplyActiveEnergyTotalImportT2,
        SupplyBusinessUsageFee,
        SupplyRenewableEnergyFee,
      };
    }
  }

  protected virtual IEnumerable<CalculationItemEntity> AdditionalUsageItems
  {
    get { return []; }
  }
}

public abstract class MeteredNetworkUserCalculationEntity<TNetworkUserCatalogue>
  : MeteredNetworkUserCalculationEntity
  where TNetworkUserCatalogue : NetworkUserCatalogueEntity
{
  public TNetworkUserCatalogue ConcreteUsageNetworkUserCatalogue { get; set; } =
    default!;

  public override NetworkUserCatalogueEntity UsageNetworkUserCatalogue
  {
    get { return ConcreteUsageNetworkUserCatalogue; }
  }
}
