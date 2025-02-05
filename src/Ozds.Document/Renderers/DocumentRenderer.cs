using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using Ozds.Document.Components;
using Ozds.Document.Entities;
using Ozds.Document.Renderers.Abstractions;

namespace Ozds.Document.Renderers.Implementations;

public class DocumentRenderer(
  IDocumentRenderer documentRenderer,
  IServiceProvider serviceProvider,
  ILoggerFactory loggerFactory
)
{
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
  public async Task<byte[]?> RenderCalculatedNetworkUserInvoice(
    CalculatedNetworkUserInvoiceEntity entity
  )
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
  {
    await using var htmlRenderer =
      new HtmlRenderer(serviceProvider, loggerFactory);

    var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
    {
      var dictionary = new Dictionary<string, object?>
      {
        { nameof(NetworkUserCalculationDocument.Entity), entity }
      };

      var parameters = ParameterView.FromDictionary(dictionary);
      var output = await htmlRenderer.RenderComponentAsync<NetworkUserInvoiceDocument>(parameters);

      return output.ToHtmlString();
    });

    return await documentRenderer.RenderHtmlToPdf(html);
  }
}
