using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public record CalculatedLoactionInvoiceModel(
  List<NetworkUserCalculationModel> NetworkUserCalculations,
  LocationInvoiceModel Invoice
);
