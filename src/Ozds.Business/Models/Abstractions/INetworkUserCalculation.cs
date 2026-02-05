using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Abstractions;

public interface INetworkUserCalculation : ICalculation
{
  public string MeterId { get; }
  public IMeter ArchivedMeter { get; }
  public string UsageNetworkUserCatalogueId { get; }
  public string SupplyRegulatoryCatalogueId { get; }
  public string NetworkUserInvoiceId { get; }
  public string NetworkUserMeasurementLocationId { get; }
  public NetworkUserCatalogueModel ArchivedUsageNetworkUserCatalogue { get; }
  public RegulatoryCatalogueModel ArchivedSupplyRegulatoryCatalogue { get; }

  public NetworkUserMeasurementLocationModel ArchivedNetworkUserMeasurementLocation { get; }
}
