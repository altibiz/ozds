using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Ozds.Document.Components;
using Ozds.Document.Renderers.Abstractions;

namespace Ozds.Document.Renderers.Implementations;

public class AspNetCoreComponentsComponentToHtmlRenderer(
  IServiceProvider serviceProvider,
  ILoggerFactory loggerFactory
) : IComponentToHtmlRenderer
{

  public async Task<string?> RenderPageToHtml(
    Type type,
    Dictionary<string, object?> parameters,
    CancellationToken cancellationToken
  )
  {
    return await RenderToHtml(
      typeof(DocumentIndex),
      nameof(DocumentIndex.Type),
      nameof(DocumentIndex.Parameters),
      type,
      parameters,
      cancellationToken
    );
  }

  public async Task<string?> RenderComponentToHtml(
    Type type,
    Dictionary<string, object?> parameters,
    CancellationToken cancellationToken
  )
  {
    return await RenderToHtml(
      typeof(DocumentView),
      nameof(DocumentView.Type),
      nameof(DocumentView.Parameters),
      type,
      parameters,
      cancellationToken
    );
  }

#pragma warning disable S1172 // Unused method parameters should be removed
  private async Task<string?> RenderToHtml(
    Type rootType,
    string typeParameterName,
    string parametersParameterName,
    Type type,
    Dictionary<string, object?> parameters,
    CancellationToken cancellationToken
  )
#pragma warning restore S1172 // Unused method parameters should be removed
  {
    await using var htmlRenderer =
      new HtmlRenderer(serviceProvider, loggerFactory);
    var indexParameters = new Dictionary<string, object?>()
      {
        { typeParameterName, type },
        { parametersParameterName, parameters },
      };
    var parameterView = ParameterView.FromDictionary(indexParameters);
    var html = await htmlRenderer.Dispatcher.InvokeAsync(
      async () =>
      {
        var output = await htmlRenderer
          .RenderComponentAsync(rootType, parameterView);
        return output.ToHtmlString();
      });
    return html;
  }
}
