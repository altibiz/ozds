using Microsoft.AspNetCore.Components;
using Ozds.Business.Models;
using Ozds.Business.Mutations;
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
      .GetRequiredService<ModelQueries>()
      .Read<NetworkUserInvoiceModel>(0, CancellationToken);

    var calculated = await ScopedServices
      .GetRequiredService<InvoiceQueries>()
      .ReadCalculatedById(invoice.Items.First().Id, CancellationToken);

    var pdf = await ScopedServices
      .GetRequiredService<DocumentMutations>()
      .CreatePdfForNetworkUserInvoice(calculated!, CancellationToken);

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
