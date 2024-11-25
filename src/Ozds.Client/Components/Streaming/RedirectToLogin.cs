using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Streaming;

public class RedirectToLogin : OzdsComponentBase
{
  protected override void OnInitialized()
  {
    NavigateToLogin();
  }
}
