using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Ozds.Document.Entities;
using Ozds.Document.Renderers.Abstractions;

namespace Ozds.Document.Renderers.Implementations;

public class DocumentRenderer(
  IDocumentRenderer documentRenderer,
  IServiceProvider serviceProvider,
  ILoggerFactory loggerFactory
)
{
  public async Task<byte[]?> RenderCalculatedNetworkUserInvoice(
    CalculatedNetworkUserInvoiceEntity entity
  )
  {
    await using var htmlRenderer =
      new HtmlRenderer(serviceProvider, loggerFactory);

    var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
    {
      var dictionary = new Dictionary<string, object?>
      {
        { "Model", entity }
      };

      var parameters = ParameterView.FromDictionary(dictionary);
      var output = await htmlRenderer.RenderComponentAsync<NetworkUserInvoiceDocument>(parameters);

      return output.ToHtmlString();
    });
  }
}
