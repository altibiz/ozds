namespace Ozds.Data.Entities.Composite;

public record NetworkUserInvoiceIssuingBasisEntity(
  LocationEntity Location,
  NetworkUserEntity NetworkUser,
  RegulatoryCatalogueEntity RegulatoryCatalogue,
  DateTimeOffset FromDate,
  DateTimeOffset ToDate,
  List<NetworkUserCalculationBasisEntity> NetworkUserCalculationBases
);
