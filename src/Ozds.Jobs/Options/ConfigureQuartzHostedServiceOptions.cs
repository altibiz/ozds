using Microsoft.Extensions.Options;
using Quartz;

namespace Ozds.Jobs.Options;

public class ConfigureQuartzHostedServiceOptions
  : IConfigureOptions<QuartzHostedServiceOptions>
{
  public void Configure(QuartzHostedServiceOptions options)
  {
    options.AwaitApplicationStarted = true;
  }
}
