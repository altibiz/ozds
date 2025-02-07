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

    var documentRenderer = serviceProvider
      .GetRequiredService<DocumentRenderer>();

    var factory = new CalculatedNetworkUserInvoiceEntityFactory();
    var entities = factory.Create();

    foreach (var entity in entities)
    {
      var html = await documentRenderer
        .RenderCalculatedNetworkUserInvoiceToHtml(entity);
      Assert.NotNull(html);
      await File.WriteAllTextAsync(
        Path.Combine(
          Environment.CurrentDirectory,
          $"invoice.html"),
        html);

      var pdf = await documentRenderer
        .RenderCalculatedNetworkUserInvoiceToPdf(entity);
      Assert.NotNull(pdf);
      await File.WriteAllBytesAsync(
        Path.Combine(
          Environment.CurrentDirectory,
          $"invoice.pdf"),
        pdf);
    }
  }
}
