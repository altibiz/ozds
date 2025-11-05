using Ozds.Assets.Extensions;
using Ozds.Document.Extensions;
using Ozds.Document.Renderers.Implementations;
using Ozds.Time.Extensions;

namespace Ozds.Document.Test.Renderer;

public class DocumentRendererTest
{
  [Test]
  public async Task RendersCalculatedNetworkUserInvoiceTest(
    CancellationToken cancellationToken
  )
  {
    var builder = Host.CreateApplicationBuilder();
    builder.Services.AddLogging();
    builder.AddOzdsDocument();
    builder.AddOzdsTime();
    builder.AddOzdsAssets();
    var host = builder.Build();

    using var scope = host.Services.CreateScope();

    var documentRenderer = scope.ServiceProvider
      .GetRequiredService<DocumentRenderer>();

    var factory = new CalculatedNetworkUserInvoiceEntityFactory();
    var entities = factory.Create();

    foreach (var entity in entities)
    {
      var html = await documentRenderer
        .RenderCalculatedNetworkUserInvoiceToHtml(
          entity, cancellationToken);
      html.Should().NotBeNull();
      var pdf = await documentRenderer
        .RenderCalculatedNetworkUserInvoiceToPdf(
          entity, cancellationToken);
      pdf.Should().NotBeNull();
    }
  }
}
