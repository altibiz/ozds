using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities.Composite;

public record CalculatedNetworkUserInvoiceEntity(
  List<NetworkUserCalculationEntity> Calculations,
  NetworkUserInvoiceEntity Invoice
);
