namespace Ozds.Business.Models.Composite;

public record NetworkUserInvoiceIssuingBasisModel(
  LocationModel Location,
  NetworkUserModel NetworkUser,
  RegulatoryCatalogueModel RegulatoryCatalogue,
  DateTimeOffset FromDate,
  DateTimeOffset ToDate,
  List<NetworkUserCalculationBasisModel> NetworkUserCalculationBases
);
