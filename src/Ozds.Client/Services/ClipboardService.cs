using Microsoft.JSInterop;

namespace Ozds.Client.Services;

public class ClipboardService(IJSRuntime _jsRuntime)
{
  public ValueTask<string> ReadTextAsync()
  {
    return _jsRuntime.InvokeAsync<string>("navigator.clipboard.readText");
  }

  public ValueTask WriteTextAsync(string text)
  {
    return _jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
  }
}
