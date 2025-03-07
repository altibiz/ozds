using Ozds.Document.Extensions;
using Ozds.Document.Renderers.Implementations;

namespace Ozds.Document.Test.Renderer;

public class DocumentRendererTest
{
  [Fact]
  public async Task RendersCalculatedNetworkUserInvoiceTest()
  {
    var serviceCollection = new ServiceCollection();
    serviceCollection.AddLogging();
    serviceCollection.AddOzdsDocument(null!);
    serviceCollection.BuildServiceProvider();
    var serviceProvider = serviceCollection.BuildServiceProvider();

    await using var scope = serviceProvider.CreateAsyncScope();

    var documentRenderer = scope.ServiceProvider
      .GetRequiredService<DocumentRenderer>();

    var factory = new CalculatedNetworkUserInvoiceEntityFactory();
    var entities = factory.Create();

    foreach (var entity in entities)
    {
      var html = await documentRenderer
        .RenderCalculatedNetworkUserInvoiceToHtml(
          entity, CancellationToken.None);
      Assert.NotNull(html);
      var pdf = await documentRenderer
        .RenderCalculatedNetworkUserInvoiceToPdf(
          entity, CancellationToken.None);
      Assert.NotNull(pdf);
    }
  }
}
