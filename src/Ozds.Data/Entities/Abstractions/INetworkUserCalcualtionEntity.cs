namespace Ozds.Data.Entities.Abstractions;

public interface INetworkUserCalculationEntity : ICalculationEntity
{
  public string NetworkUserInvoiceId { get; }

  public decimal UsageFeeTotal_EUR { get; }

  public decimal SupplyFeeTotal_EUR { get; }

  public string SupplyRegulatoryCatalogueId { get; }

  public string NetworkUserMeasurementLocationId { get; }
}
