using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace Ozds.Client.Options;

public class ConfigureHubOptions(
  IHostEnvironment environment
) : IConfigureOptions<HubOptions>
{
  public void Configure(HubOptions options)
  {
    if (environment.IsDevelopment())
    {
      options.EnableDetailedErrors = true;
    }
  }
}
