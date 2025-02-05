using Ozds.Document.Extensions;
using Ozds.Document.Renderers.Implementations;

namespace Ozds.Document.Test.Renderer;

public class DocumentRendererTest
{
  [Fact]
  public async Task RendersCalculatedNetworkUserInvoiceTest()
  {
    var serviceProvider = new ServiceCollection()
      .AddLogging()
      .AddOzdsDocument(null!)
      .BuildServiceProvider();

    var documentRenderer = serviceProvider
      .GetRequiredService<DocumentRenderer>();

    var factory = new CalculatedNetworkUserInvoiceEntityFactory();
    var entities = factory.Create();

    foreach (var entity in entities)
    {
      var html = await documentRenderer
        .RenderCalculatedNetworkUserInvoice(entity);

      Assert.NotNull(html);
    }
  }
}
