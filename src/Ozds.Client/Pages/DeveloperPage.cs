using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Pages;

public partial class DeveloperPage : OzdsComponentBase
{
  [Inject]
  private ILogger<DeveloperPage> Logger { get; set; } = default!;

  [Inject]
  private IHostEnvironment Environment { get; set; } = default!;

  [Inject]
  private IHostApplicationLifetime ApplicationLifetime { get; set; } = default!;

  private void OnThrowClick()
  {
    Logger.LogDebug("Throw clicked");
    throw new InvalidOperationException();
  }

  private void OnStopClick()
  {
    ApplicationLifetime.StopApplication();
  }
}
