namespace Ozds.Business.Models.Composite;

public record NetworkUserInvoiceIssuingBasisModel(
  LocationModel Location,
  NetworkUserModel NetworkUser,
  DateTimeOffset FromDate,
  DateTimeOffset ToDate,
  List<NetworkUserCalculationBasisModel> NetworkUserCalculationBases
);
