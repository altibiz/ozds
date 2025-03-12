using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public class AnalysisBasisModel
{
  public RepresentativeModel? Representative { get; set; } = default!;

  public DateTimeOffset FromDate { get; set; }

  public DateTimeOffset ToDate { get; set; }

  public LocationModel Location { get; set; } = default!;

  public NetworkUserModel? NetworkUser { get; set; } = default!;

  public MeasurementLocationModel MeasurementLocation { get; set; } =
    default!;

  public MeterModel Meter { get; set; } = default!;

  public List<CalculationModel> Calculations { get; set; } = default!;

  public List<InvoiceModel> Invoices { get; set; } = default!;

  public MeasurementModel LastMeasurement { get; set; } = default!;

  public List<AggregateModel> MonthlyAggregates { get; set; } = default!;
}
