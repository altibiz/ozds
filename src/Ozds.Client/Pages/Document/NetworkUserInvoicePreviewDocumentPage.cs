using Microsoft.AspNetCore.Components;
using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries;
using Ozds.Business.Time;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Pages;

public partial class NetworkUserInvoicePreviewDocumentPage : OzdsComponentBase
{
  [Parameter]
  public string NetworkUserId { get; set; } = default!;

  [Parameter]
  public int Year { get; set; }

  [Parameter]
  public int Month { get; set; }

  private async Task<CalculatedNetworkUserInvoiceModelWithHtml?> OnLoadAsync()
  {
    var (from, to) = DateTimeOffsetExtensions.GetMonthRange(Year, Month);

    var invoice = await ScopedServices
      .GetRequiredService<INetworkUserInvoiceIssuer>()
      .PreviewNetworkUserInvoiceAsync(
        NetworkUserId,
        from,
        to,
        CancellationToken
      );
    if (invoice is null)
    {
      return null;
    }

    var html = await ScopedServices
      .GetRequiredService<DocumentQueries>()
      .ReadHtmlForNetworkUserInvoice(invoice, CancellationToken);
    if (html is null)
    {
      return null;
    }

    return new CalculatedNetworkUserInvoiceModelWithHtml
    {
      Invoice = invoice,
      Html = html
    };
  }

  private sealed class CalculatedNetworkUserInvoiceModelWithHtml
  {
    public CalculatedNetworkUserInvoiceModel Invoice { get; set; } = default!;

    public string Html { get; set; } = default!;
  }
}
