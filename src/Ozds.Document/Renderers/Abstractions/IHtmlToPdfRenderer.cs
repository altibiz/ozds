namespace Ozds.Document.Renderers.Abstractions;

public interface IHtmlToPdfRenderer
{
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
  public Task<byte[]?> RenderHtmlToPdf(string html);
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
}
