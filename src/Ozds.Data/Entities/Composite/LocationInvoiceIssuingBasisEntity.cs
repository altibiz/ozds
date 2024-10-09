namespace Ozds.Data.Entities.Composite;

public record LocationInvoiceIssuingBasisEntity(
  List<LocationNetworkUserCalculationBasisEntity> NetworkUserCalculationBases,
  LocationEntity Location,
  DateTimeOffset FromDate,
  DateTimeOffset ToDate
);
