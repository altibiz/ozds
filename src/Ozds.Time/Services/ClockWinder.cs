using System.Globalization;
using Microsoft.Extensions.Options;
using Ozds.Time.Options;

namespace Ozds.Time.Clock;

public class ClockWinder(
  IHostEnvironment environment,
  IOptions<OzdsTimeOptions> options,
  ILogger<ClockWinder> logger
) : IHostedService
{
  public TimeSpan Offset { get; private set; } = TimeSpan.Zero;

  public Task StartAsync(CancellationToken cancellationToken)
  {
    if (!environment.IsDevelopment())
    {
      return Task.CompletedTask;
    }

    if (
      !DateTimeOffset.TryParse(
        options.Value.RewindTimeStart,
        CultureInfo.InvariantCulture,
        out var rewindTimeStart
      )
    )
    {
      return Task.CompletedTask;
    }

    rewindTimeStart = rewindTimeStart.ToUniversalTime();

    var now = DateTimeOffset.UtcNow;

    Offset = rewindTimeStart - now;
    logger.LogWarning(
      "Rewind time start is set to {RewindTimeStart}. "
        + "Current time is {CurrentTime}. "
        + "Offset is {Offset}.",
      rewindTimeStart,
      now,
      Offset
    );

    return Task.CompletedTask;
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    return Task.CompletedTask;
  }
}
