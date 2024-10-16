using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public record AnalysisBasisModel(
  LocationModel Location,
  NetworkUserModel? NetworkUser,
  MeasurementLocationModel MeasurementLocation,
  MeterModel Meter,
  List<CalculationModel> Calculations,
  List<InvoiceModel> Invoices,
  MeasurementModel LastMeasurement,
  List<AggregateModel> MonthlyAggregates
);
