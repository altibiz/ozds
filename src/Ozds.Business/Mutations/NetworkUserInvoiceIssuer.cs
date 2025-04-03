using System.Globalization;
using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Localization.Abstractions;
using Ozds.Business.Models.Composite;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Business.Queries;

namespace Ozds.Business.Mutations;

public class NetworkUserInvoiceIssuer(
  IServiceScopeFactory factory,
  INetworkUserInvoiceCalculator invoiceCalculator,
  ILocalizer localizer
) : IMutations
{
  public async Task<CalculatedNetworkUserInvoiceModel>
    PreviewNetworkUserInvoiceAsync(
      string networkUserId,
      DateTimeOffset dateFrom,
      DateTimeOffset dateTo,
      CancellationToken cancellationToken
    )
  {
    await using var scope = factory.CreateAsyncScope();
    var billingQueries = scope.ServiceProvider
      .GetRequiredService<BillingQueries>();
    var basis = await billingQueries
      .IssuingBasisForNetworkUser(
        networkUserId,
        dateFrom,
        dateTo,
        cancellationToken
      );
    var invoice = invoiceCalculator.Calculate(basis);
    var culture = CultureInfo.CreateSpecificCulture("hr-HR");
    var previewText = localizer.TranslateForCulture(
      culture,
      "This invoice is a preview."
    );
    invoice.Invoice.Remark = $@"
      <div>
        <p><strong>{previewText}</strong></p>
        <p>{invoice.Invoice.Remark}</p>
      </div>
    ";
    return invoice;
  }

  public async Task<CalculatedNetworkUserInvoiceModel>
    IssueNetworkUserInvoiceAsync(
      string networkUserId,
      DateTimeOffset dateFrom,
      DateTimeOffset dateTo,
      CancellationToken cancellationToken
    )
  {
    await using var scope = factory.CreateAsyncScope();
    var billingQueries = scope.ServiceProvider
      .GetRequiredService<BillingQueries>();
    var mutations = scope.ServiceProvider
      .GetRequiredService<CalculatedInvoiceMutations>();
    var basis = await billingQueries
      .IssuingBasisForNetworkUser(
        networkUserId,
        dateFrom,
        dateTo,
        cancellationToken
      );
    var invoice = invoiceCalculator.Calculate(basis);
    invoice = await mutations.CreateCalculatedInvoice(invoice, cancellationToken);
    return invoice;
  }
}
