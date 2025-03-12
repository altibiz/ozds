using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities.Composite;

public class AnalysisBasisEntity
{
  public RepresentativeEntity? Representative { get; set; } = default!;

  public DateTimeOffset FromDate { get; set; }

  public DateTimeOffset ToDate { get; set; }

  public LocationEntity Location { get; set; } = default!;

  public NetworkUserEntity NetworkUser { get; set; } = default!;

  public MeasurementLocationEntity MeasurementLocation { get; set; } =
    default!;

  public MeterEntity Meter { get; set; } = default!;

  public List<CalculationEntity> Calculations { get; set; } = default!;

  public List<InvoiceEntity> Invoices { get; set; } = default!;

  public MeasurementEntity LastMeasurement { get; set; } = default!;

  public List<AggregateEntity> MonthlyAggregates { get; set; } = default!;
}
