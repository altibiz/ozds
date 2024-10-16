using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public record CalculatedNetworkUserInvoiceModel(
  List<NetworkUserCalculationModel> Calculations,
  NetworkUserInvoiceModel Invoice
);
