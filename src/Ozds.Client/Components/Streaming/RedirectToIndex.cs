using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Streaming;

public class RedirectToIndex : OzdsComponentBase
{
  protected override void OnInitialized()
  {
    NavigateToIndex();
  }
}
