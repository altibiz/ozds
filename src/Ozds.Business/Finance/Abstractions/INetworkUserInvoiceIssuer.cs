namespace Ozds.Business.Finance.Abstractions;

public interface INetworkUserInvoiceIssuer
{
  public Task IssueNetworkUserInvoiceAsync(
    string networkUserId,
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo
  );
}
