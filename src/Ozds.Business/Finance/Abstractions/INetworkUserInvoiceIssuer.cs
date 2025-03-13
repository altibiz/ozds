using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Abstractions;

public interface INetworkUserInvoiceIssuer
{
  public Task<CalculatedNetworkUserInvoiceModel> PreviewNetworkUserInvoiceAsync(
    string networkUserId,
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    CancellationToken cancellationToken
  );

  public Task IssueNetworkUserInvoiceAsync(
    string networkUserId,
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    CancellationToken cancellationToken
  );
}
