using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Ozds.Server;

public class EmptyView : IView
{
  public string Path { get; } = "";

  public Task RenderAsync(ViewContext _) => Task.CompletedTask;
}
