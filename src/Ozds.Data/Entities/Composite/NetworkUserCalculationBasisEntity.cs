using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities.Composite;

public class NetworkUserCalculationBasisEntity
{
  public DateTimeOffset FromDate { get; set; }

  public DateTimeOffset ToDate { get; set; }

  public List<AggregateEntity> Aggregates { get; set; } = default!;

  public LocationEntity Location { get; set; } = default!;

  public NetworkUserEntity NetworkUser { get; set; } = default!;

  public NetworkUserMeasurementLocationEntity MeasurementLocation
  {
    get;
    set;
  } =
    default!;

  public NetworkUserCatalogueEntity UsageNetworkUserCatalogue
  {
    get;
    set;
  } = default!;

  public RegulatoryCatalogueEntity SupplyRegulatoryCatalogue
  {
    get;
    set;
  } = default!;

  public MeterEntity Meter { get; set; } = default!;
}
