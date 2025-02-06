using Ozds.Document.Components;
using Ozds.Document.Entities;
using Ozds.Document.Renderers.Abstractions;

namespace Ozds.Document.Renderers.Implementations;

public class DocumentRenderer(
  IHtmlToPdfRenderer documentRenderer,
  IComponentToHtmlRenderer componentToHtmlRenderer
)
{
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
  public async Task<byte[]?> RenderCalculatedNetworkUserInvoice(
    CalculatedNetworkUserInvoiceEntity entity
  )
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
  {
    var html = await componentToHtmlRenderer.RenderComponentToHtml(
      typeof(NetworkUserInvoiceDocument),
      new Dictionary<string, object?>
      {
        [nameof(NetworkUserInvoiceDocument.Entity)] = entity
      }
    );
    if (html is null)
    {
      return null;
    }

    var pdf = await documentRenderer.RenderHtmlToPdf(html);
    if (pdf is null)
    {
      return null;
    }

    return pdf;
  }
}
