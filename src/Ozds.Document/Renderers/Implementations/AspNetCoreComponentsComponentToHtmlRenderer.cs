using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Ozds.Document.Renderers.Abstractions;

namespace Ozds.Document.Renderers.Implementations;

public class AspNetCoreComponentsComponentToHtmlRenderer(
  IServiceProvider serviceProvider,
  ILoggerFactory loggerFactory,
  ILogger<AspNetCoreComponentsComponentToHtmlRenderer> logger
) : IComponentToHtmlRenderer
{
  public async Task<string?> RenderComponentToHtml(
    Type type,
    Dictionary<string, object?> parameters
  )
  {
    try
    {
      await using var htmlRenderer =
        new HtmlRenderer(serviceProvider, loggerFactory);
      var parameterView = ParameterView.FromDictionary(parameters);
      var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
      {
        try
        {
          var output = await htmlRenderer
            .RenderComponentAsync(type, parameterView);
          return output.ToHtmlString();
        }
        catch (Exception ex)
        {
          logger.LogError(ex, "Error while rendering component to html");
          return null;
        }
      });
      return html;
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Error while rendering component to html");
      return null;
    }
  }
}
