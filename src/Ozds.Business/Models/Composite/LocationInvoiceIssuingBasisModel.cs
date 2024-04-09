namespace Ozds.Business.Models.Composite;

public record LocationInvoiceIssuingBasisModel(
  List<LocationNetworkUserCalculationBasisModel> NetworkUserCalculationBases,
  LocationModel Location,
  DateTimeOffset FromDate,
  DateTimeOffset ToDate
);
