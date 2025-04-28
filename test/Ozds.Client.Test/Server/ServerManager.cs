using System.Diagnostics;
using Microsoft.Extensions.Options;
using Ozds.Client.Test.Options;

namespace Ozds.Client.Test.Server;

public sealed class ServerManager(
  ILogger<ServerManager> logger,
  IOptions<OzdsClientTestOptions> options,
  IHttpClientFactory factory
) : IHostedService, IDisposable
{
  private CancellationTokenSource? appCancellationTokenSource;

  private Task? serverTask;

  public void Dispose()
  {
    appCancellationTokenSource?.Dispose();
  }

  public async Task StartAsync(CancellationToken cancellationToken)
  {
    if (options.Value.Server.Startup is { } startupOptions)
    {
      appCancellationTokenSource = new CancellationTokenSource();
      serverTask = StartServer(
        startupOptions,
        appCancellationTokenSource.Token
      );
    }

    await WaitForServerToStart(options.Value.Server, cancellationToken);
  }

  public async Task StopAsync(CancellationToken cancellationToken)
  {
    if (appCancellationTokenSource is not null && serverTask is not null)
    {
      try
      {
        await appCancellationTokenSource.CancelAsync();
      }
      catch (Exception exception)
      {
        logger.LogError(exception, "Failed to cancel the server");
      }

      await serverTask;
    }
  }

  private async Task StartServer(
    OzdsClientTestServerStartupOptions options,
    CancellationToken token
  )
  {
    var processStartInfo = new ProcessStartInfo
    {
      FileName = options.Command,
      Arguments = string.Join(' ', options.Arguments),
      RedirectStandardOutput = true,
      RedirectStandardError = true,
      UseShellExecute = false,
      CreateNoWindow = false,
      WorkingDirectory = options.WorkingDirectory
    };
    foreach (var (key, value) in options.Environment)
    {
      processStartInfo.Environment.Add(key, value);
    }

    using var process = new Process
    {
      StartInfo = processStartInfo,
      EnableRaisingEvents = true
    };

    process.Start();

    if (process.HasExited)
    {
      throw new InvalidOperationException(
        $"The server process has already exited with code {process.ExitCode}");
    }

    logger.LogInformation(
      "Starting the server process with command: {Command}",
      $"{options.Command} {string.Join(' ', options.Arguments)}"
    );

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
      process.Kill(true);
      try
      {
        await process.WaitForExitAsync(CancellationToken.None);
      }
      catch (Exception exception)
        when (exception is OperationCanceledException or TaskCanceledException)
      {
        logger.LogInformation(exception, "Process cancelled");
      }
    }

    if (!process.HasExited)
    {
      var @out = await process.StandardOutput
        .ReadToEndAsync(CancellationToken.None);
      var err = await process.StandardError
        .ReadToEndAsync(CancellationToken.None);
      throw new InvalidOperationException(
        $"Process never exited"
        + $"\nOUT:\n{@out}"
        + $"\nERR:\n{err}");
    }

    // !graceful && !killed on unix && !killed on windows
    if (
      process.ExitCode != 0
      && process.ExitCode != 137
      && process.ExitCode != 1)
    {
      var @out = await process.StandardOutput
        .ReadToEndAsync(CancellationToken.None);
      var err = await process.StandardError
        .ReadToEndAsync(CancellationToken.None);
      throw new InvalidOperationException(
        $"Process exited with code {process.ExitCode}"
        + $"\nOUT:\n{@out}"
        + $"\nERR:\n{err}");
    }
  }

  private async Task WaitForServerToStart(
    OzdsClientTestServerOptions options,
    CancellationToken token
  )
  {
    var uri = options.StatusUri;

    using var client = factory.CreateClient();

    var startTime = DateTimeOffset.UtcNow;
    while (DateTimeOffset.UtcNow.Subtract(startTime).TotalSeconds
      < options.Timeout_s)
    {
      if (token.IsCancellationRequested)
      {
        return;
      }

      try
      {
        await client.GetAsync(uri, token);
        return;
      }
      catch
      {
        await Task.Delay(1000, token);
      }
    }

    throw new TimeoutException(
      $"Server at {uri} did not respond within {options.Timeout_s} seconds"
    );
  }
}
