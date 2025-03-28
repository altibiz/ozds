using Microsoft.AspNetCore.Components.Server;
using Microsoft.Extensions.Options;

namespace Ozds.Client.Options;

public class ConfigureCircuitOptions(
  IHostEnvironment environment
) : IConfigureOptions<CircuitOptions>
{
  public void Configure(CircuitOptions options)
  {
    if (environment.IsDevelopment())
    {
      options.DetailedErrors = true;
    }
  }
}
