using Ozds.Business.Models.Composite;

namespace Ozds.Business.Conversion.Abstractions;

public interface IInvoiceIssuer
{
  public bool CanIssueForNetworkUser(NetworkUserInvoiceIssuingBasisModel basis);

  public bool CanIssueForLocation(LocationInvoiceIssuingBasisModel basis);

  CalculatedNetworkUserInvoiceModel IssueForNetworkUser(
    NetworkUserInvoiceIssuingBasisModel basis
  );

  CalculatedLoactionInvoiceModel IssueForLocation(
    LocationInvoiceIssuingBasisModel basis
  );
}
