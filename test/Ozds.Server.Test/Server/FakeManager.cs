using System.Diagnostics;
using System.Runtime.InteropServices;
using Ozds.Server.Test.Options;

namespace Ozds.Server.Test.Server;

public sealed class FakeManager(
  ILogger<FakeManager> logger,
#pragma warning disable CS9113 // Parameter is unread.
  OzdsServerTestFakeOptions options,
#pragma warning restore CS9113 // Parameter is unread.
  Process process
) : IAsyncDisposable
{
  public async ValueTask DisposeAsync()
  {
    await Kill(CancellationToken.None);
    process.Dispose();
  }

  public async Task WaitForExit(
    CancellationToken token
  )
  {
    try
    {
      await process.WaitForExitAsync(token);
    }
    catch (Exception exception)
      when (exception is OperationCanceledException or TaskCanceledException)
    {
      logger.LogInformation(exception, "Process cancelled");
    }

    await Kill(token);
  }

  public async Task Stop(
    CancellationToken token
  )
  {
    await Kill(token);
  }

  private async Task Kill(
    CancellationToken token
  )
  {
    if (process.HasExited)
    {
      return;
    }

    process.Kill(true);
    try
    {
      await process.WaitForExitAsync(token);
    }
    catch (Exception exception)
      when (exception is OperationCanceledException or TaskCanceledException)
    {
      logger.LogInformation(exception, "Process cancelled");
    }

    if (!process.HasExited)
    {
      var @out = await process.StandardOutput
        .ReadToEndAsync(CancellationToken.None);
      var err = await process.StandardError
        .ReadToEndAsync(CancellationToken.None);
      throw new InvalidOperationException(
        $"Process never exited"
        + $"{Environment.NewLine}OUT:{Environment.NewLine}{@out}"
        + $"{Environment.NewLine}ERR:{Environment.NewLine}{err}");
    }

    var isAcceptableExitCode =
      process.ExitCode == 0
      || (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
        && process.ExitCode == -1)
      || (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
        && process.ExitCode == 137);
    if (!isAcceptableExitCode)
    {
      var @out = await process.StandardOutput
        .ReadToEndAsync(token);
      var err = await process.StandardError
        .ReadToEndAsync(token);
      throw new InvalidOperationException(
        $"Process exited with code {process.ExitCode}"
        + $"{Environment.NewLine}OUT:{Environment.NewLine}{@out}"
        + $"{Environment.NewLine}ERR:{Environment.NewLine}{err}");
    }
  }
}
