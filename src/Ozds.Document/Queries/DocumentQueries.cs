using Ozds.Document.Entities;
using Ozds.Document.Renderers.Implementations;

namespace Ozds.Document.Queries;

public class DocumentQueries(
  DocumentRenderer Renderer
)
{
  public async Task<string?> ReadHtmlForNetworkUserInvoice(
    CalculatedNetworkUserInvoiceEntity entity,
    CancellationToken cancellationToken
  )
  {
    var html = await Renderer.RenderCalculatedNetworkUserInvoiceToHtml(
      entity,
      cancellationToken
    );
    return html;
  }

#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
  public async Task<byte[]?> ReadPdfForNetworkUserInvoice(
    CalculatedNetworkUserInvoiceEntity entity,
    CancellationToken cancellationToken
  )
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
  {
    var pdf = await Renderer.RenderCalculatedNetworkUserInvoiceToPdf(
      entity,
      cancellationToken
    );
    return pdf;
  }
}
