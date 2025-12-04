namespace Ozds.Data.Entities.Abstractions;

public interface INetworkUserCalculationEntity : ICalculationEntity
{
  public string NetworkUserInvoiceId { get; }

  public string SupplyRegulatoryCatalogueId { get; }

  public string NetworkUserMeasurementLocationId { get; }
}
