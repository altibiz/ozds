namespace Ozds.Business.Extensions;

public static class IHosApplicationLifetimeExtensions
{
  public static async Task<bool> WaitForAppStartup(
    this IHostApplicationLifetime lifetime,
    CancellationToken stoppingToken)
  {
    var startedSource = new TaskCompletionSource();
    var stoppingSource = new TaskCompletionSource();

    using var startedRegistration = lifetime.ApplicationStarted
      .Register(startedSource.SetResult);
    using var stoppingRegistration = stoppingToken
      .Register(stoppingSource.SetResult);

    var completedTask = await Task.WhenAny(
      startedSource.Task, stoppingSource.Task);

    return completedTask == startedSource.Task;
  }

  public static async Task<bool> WaitForAppShutdown(
    this IHostApplicationLifetime lifetime,
    CancellationToken stoppingToken)
  {
    var stoppedSource = new TaskCompletionSource();
    var stoppingSource = new TaskCompletionSource();

    using var startedRegistration = lifetime.ApplicationStopped
      .Register(stoppedSource.SetResult);
    using var stoppingRegistration = stoppingToken
      .Register(stoppingSource.SetResult);

    var completedTask = await Task.WhenAny(
      stoppedSource.Task, stoppingSource.Task);

    return completedTask == stoppedSource.Task;
  }
}
