using Microsoft.AspNetCore.Components;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Pages;

public partial class NetworkUserInvoiceDocumentPage : OzdsComponentBase
{
  [Parameter]
  public string Id { get; set; } = default!;

  private async Task<CalculatedNetworkUserInvoiceModelWithHtml?> OnLoadAsync()
  {
    var invoice = await ScopedServices
      .GetRequiredService<CalculatedInvoiceQueries>()
      .ReadCalculatedNetworkUserInvoice(Id, CancellationToken);
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
