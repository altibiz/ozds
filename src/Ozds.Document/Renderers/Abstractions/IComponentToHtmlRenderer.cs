namespace Ozds.Document.Renderers.Abstractions;

public interface IComponentToHtmlRenderer
{
  public Task<string?> RenderComponentToHtml(
    Type type,
    Dictionary<string, object?> parameters
  );
}
