using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities.Composite;

public class ReportBasisEntity
{
  public LocationEntity Location { get; set; } = default!;

  public NetworkUserEntity NetworkUser { get; set; } = default!;

  public NetworkUserCatalogueEntity Catalogue { get; set; } = default!;

  public NetworkUserMeasurementLocationEntity MeasurementLocation { get; set; } =
    default!;

  public MeterEntity Meter { get; set; } = default!;
}

public class EnergyCardReportBasisEntity : ReportBasisEntity
{
  public AggregateEntity MinAggregate { get; set; } = default!;

  public AggregateEntity MaxAggregate { get; set; } = default!;
}

public class AccountingPeriodReportBasisEntity : ReportBasisEntity
{
  public AggregateEntity MinAggregate { get; set; } = default!;

  public AggregateEntity MaxAggregate { get; set; } = default!;
}

public class LoadCurveReportBasisEntity : ReportBasisEntity
{
  public List<AggregateEntity> Aggregates { get; set; } = default!;
}
