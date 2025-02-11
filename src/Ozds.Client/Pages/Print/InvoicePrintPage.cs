using Microsoft.AspNetCore.Components;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Pages;

public partial class InvoicePrintPage : OzdsComponentBase
{
  [Parameter]
  public string Id { get; set; } = default!;

  private Task<CalculatedNetworkUserInvoiceModel?> OnLoadAsync()
  {
    return ScopedServices
      .GetRequiredService<CalculatedInvoiceQueries>()
      .ReadCalculatedNetworkUserInvoice(Id, CancellationToken);
  }
}
