using Fluid;
using Microsoft.AspNetCore.Components;
using Ozds.Business.Models;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Pages;

public partial class DeveloperPage : OzdsComponentBase
{
  [Inject]
  private ILogger<DeveloperPage> Logger { get; set; } = default!;

  [Inject]
  private IHostEnvironment Environment { get; set; } = default!;

  [Inject]
  private IHostApplicationLifetime ApplicationLifetime { get; set; } = default!;

  private void OnThrowClick()
  {
    Logger.LogDebug("Throw clicked");
    throw new InvalidOperationException();
  }

  private void OnStopClick()
  {
    ApplicationLifetime.StopApplication();
  }

  private async Task OnPdfClick()
  {
    var invoice = await ScopedServices
      .GetRequiredService<AuditableQueries>()
      .Read<NetworkUserInvoiceModel>(
        0,
        CancellationToken.None
      );

    var pdf = await ScopedServices
      .GetRequiredService<DocumentQueries>()
      .ReadPdfForNetworkUserInvoice(
        invoice.Items.First().Id,
        CancellationToken.None
      );

    if (pdf is null)
    {
      Logger.LogError("PDF is null");
    }
    else
    {
      Logger.LogDebug("PDF: {Length} bytes", pdf.Length);
    }
  }
}
