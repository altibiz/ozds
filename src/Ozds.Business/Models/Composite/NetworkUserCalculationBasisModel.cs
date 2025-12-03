using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public class NetworkUserCalculationBasisModel : IComposite
{
  public DateTimeOffset FromDate { get; set; }

  public DateTimeOffset ToDate { get; set; }

  public DateTimeOffset MeasuredFromDate { get; set; }

  public DateTimeOffset MeasuredToDate { get; set; }

  public DateTimeOffset BilledFromDate { get; set; }

  public DateTimeOffset BilledToDate { get; set; }

  public List<AggregateModel> Aggregates { get; set; } = default!;

  public LocationModel Location { get; set; } = default!;

  public NetworkUserModel NetworkUser { get; set; } = default!;

  public NetworkUserMeasurementLocationModel MeasurementLocation { get; set; } =
    default!;

  public NetworkUserCatalogueModel UsageNetworkUserCatalogue { get; set; } =
    default!;

  public RegulatoryCatalogueModel SupplyRegulatoryCatalogue { get; set; } =
    default!;

  public MeterModel Meter { get; set; } = default!;
}
