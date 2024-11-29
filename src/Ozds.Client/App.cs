using Microsoft.AspNetCore.Components;
using Ozds.Client.State;

namespace Ozds.Client;

public partial class App : ComponentBase
{
  [Parameter]
  public string LogoutToken { get; set; } = default!;
}
