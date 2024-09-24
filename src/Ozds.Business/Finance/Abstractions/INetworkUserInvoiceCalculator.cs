using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Abstractions;

public interface INetworkUserInvoiceCalculator
{
  public CalculatedNetworkUserInvoiceModel Calculate(
    NetworkUserInvoiceIssuingBasisModel basis
  );
}
