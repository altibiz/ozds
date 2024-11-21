using Ozds.Business.Conversion;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Composite;

namespace Ozds.Business.Models.Composite;

public record AnalysisBasisModel(
  RepresentativeModel Representative,
  DateTimeOffset FromDate,
  DateTimeOffset ToDate,
  LocationModel Location,
  NetworkUserModel? NetworkUser,
  MeasurementLocationModel MeasurementLocation,
  MeterModel Meter,
  List<CalculationModel> Calculations,
  List<InvoiceModel> Invoices,
  MeasurementModel LastMeasurement,
  List<AggregateModel> MonthlyAggregates,
  List<AggregateModel> MonthlyMaxPowerAggregates
);

public static class AnalysisBasisModelExtensions
{
  public static AnalysisBasisModel ToModel(
    this AnalysisBasisEntity entity
  )
  {
    return new AnalysisBasisModel(
      entity.Representative.ToModel(),
      entity.FromDate,
      entity.ToDate,
      entity.Location.ToModel(),
      entity.NetworkUser is { }
        ? entity.NetworkUser.ToModel()
        : null,
      entity.MeasurementLocation.ToModel(),
      entity.Meter.ToModel(),
      entity.Calculations
        .Select(calculation => calculation.ToModel())
        .ToList(),
      entity.Invoices
        .Select(invoice => invoice.ToModel())
        .ToList(),
      entity.LastMeasurement.ToModel(),
      entity.MonthlyAggregates
        .Select(aggregate => aggregate.ToModel())
        .ToList(),
      entity.MonthlyMaxPowerAggregates
        .Select(aggregate => aggregate.ToModel())
        .ToList()
    );
  }
}
