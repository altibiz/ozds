using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public class ReportBasisModel : IComposite
{
  public LocationModel Location { get; set; } = default!;

  public NetworkUserModel NetworkUser { get; set; } = default!;

  public NetworkUserCatalogueModel Catalogue { get; set; } = default!;

  public NetworkUserMeasurementLocationModel MeasurementLocation { get; set; } =
    default!;

  public MeterModel Meter { get; set; } = default!;
}

public class EnergyCardReportBasisModel : ReportBasisModel
{
  public AggregateModel MinAggregate { get; set; } = default!;

  public AggregateModel MaxAggregate { get; set; } = default!;
}

public class AccountingPeriodReportBasisModel : ReportBasisModel
{
  public AggregateModel MinAggregate { get; set; } = default!;

  public AggregateModel MaxAggregate { get; set; } = default!;
}

public class LoadCurveReportBasisModel : ReportBasisModel
{
  public List<AggregateModel> Aggregates { get; set; } = default!;
}
