namespace Ozds.Business.Models.Composite;

public record LocationInvoiceIssuingBasisModel(
  List<LocationCalculationBasisModel> CalculationBases,
  LocationModel Location,
  DateTimeOffset FromDate,
  DateTimeOffset ToDate
);
