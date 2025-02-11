namespace Ozds.Document.Entities;

public abstract class NetworkUserCalculationEntity : CalculationEntity
{
  public string MeterTitle { get; set; } = default!;

  public string MeterId { get; set; } = default!;

  public string MeasurementLocationTitle { get; set; } = default!;

  public string MeasurementLocationId { get; set; } = default!;

  public UsageMeterFeeCalculationItemEntity
    UsageMeterFee { get; set; } = default!;

  public SupplyActiveEnergyTotalImportT1CalculationItemEntity
    SupplyActiveEnergyTotalImportT1 { get; set; } = default!;

  public SupplyActiveEnergyTotalImportT2CalculationItemEntity
    SupplyActiveEnergyTotalImportT2 { get; set; } = default!;

  public SupplyBusinessUsageCalculationItemEntity
    SupplyBusinessUsageFee { get; set; } = default!;

  public SupplyRenewableEnergyCalculationItemEntity
    SupplyRenewableEnergyFee { get; set; } = default!;

  public abstract NetworkUserCatalogueEntity UsageNetworkUserCatalogue { get; }

  public string UsageNetworkUserCatalogueId { get; set; } = default!;

  public string SupplyRegulatoryCatalogueId { get; set; } = default!;

  public string NetworkUserInvoiceId { get; set; } = default!;

  public RegulatoryCatalogueEntity SupplyRegulatoryCatalogue { get; set; } =
    default!;

  public decimal UsageFeeTotal_EUR { get; set; }

  public decimal SupplyFeeTotal_EUR { get; set; }

  public virtual IEnumerable<CalculationItemEntity> UsageItems
  {
    get
    {
      return AdditionalUsageItems
        .AsEnumerable()
        .Concat(
          new CalculationItemEntity[]
          {
            UsageMeterFee
          });
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
        SupplyRenewableEnergyFee
      };
    }
  }

  protected abstract IEnumerable<CalculationItemEntity> AdditionalUsageItems
  {
    get;
  }
}

public abstract class NetworkUserCalculationEntity<TNetworkUserCatalogue>
  : NetworkUserCalculationEntity
  where TNetworkUserCatalogue : NetworkUserCatalogueEntity
{
  public TNetworkUserCatalogue
    ConcreteUsageNetworkUserCatalogue { get; set; } = default!;

  public override NetworkUserCatalogueEntity UsageNetworkUserCatalogue
  {
    get { return ConcreteUsageNetworkUserCatalogue; }
  }
}
