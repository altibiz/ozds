using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public record CalculatedLoactionInvoiceModel(
  List<CalculationModel> Calculations,
  LocationInvoiceModel Invoice
);
