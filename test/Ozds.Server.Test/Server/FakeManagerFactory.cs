using System.Diagnostics;
using Microsoft.Extensions.Options;
using Ozds.Server.Test.Options;

namespace Ozds.Server.Test.Server;

public enum FakeInterval
{
  Hour,
  Day,
  Week,
  Month,
  Season,
  Year
}

public class FakeManagerFactory(
  IServiceProvider services,
  ILogger<FakeManagerFactory> logger,
  IOptions<OzdsServerTestOptions> options
)
{
  public FakeManager StartPush()
  {
    return Start(
      ["push"],
      new Dictionary<string, string>()
    );
  }

  public FakeManager StartSeed(
    FakeInterval interval
  )
  {
    return Start(
      ["insert", "--interval", IntervalToArgument(interval)],
      new Dictionary<string, string>()
    );
  }

  public FakeManager StartInsert(
    FakeInterval interval,
    bool aggregatesOnly
  )
  {
    var arguments = new List<string>
    {
      "insert",
      "--interval",
      IntervalToArgument(interval)
    };
    if (aggregatesOnly)
    {
      arguments.Add("--aggregates-only");
    }

    return Start(
      arguments,
      new Dictionary<string, string>()
    );
  }

  public FakeManager StartAltibiz()
  {
    return Start(
      ["altibiz"],
      new Dictionary<string, string>()
    );
  }

  public FakeManager Start(
    IEnumerable<string> additionalArguments,
    IEnumerable<KeyValuePair<string, string>> additionalEnvironment
  )
  {
    var finalOptions = new OzdsServerTestFakeOptions
    {
      Command = options.Value.Fake.Command,
      Arguments = options.Value.Fake.Arguments
        .Select(x => x.ToString())
        .Append("--")
        .Concat(additionalArguments)
        .ToList(),
      Environment = options.Value.Fake.Environment
        .Select(
          x => new KeyValuePair<string, string>(
            x.Key.ToString(),
            x.Value.ToString()))
        .Concat(additionalEnvironment)
        .ToDictionary(x => x.Key, x => x.Value),
      WorkingDirectory = options.Value.Fake.WorkingDirectory
    };

    var processStartInfo = new ProcessStartInfo
    {
      FileName = finalOptions.Command,
      Arguments = string.Join(' ', finalOptions.Arguments),
      RedirectStandardOutput = true,
      RedirectStandardError = true,
      UseShellExecute = false,
      CreateNoWindow = false,
      WorkingDirectory = finalOptions.WorkingDirectory
    };
    foreach (var (key, value) in finalOptions.Environment)
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
        $"The fake process has already exited with code {process.ExitCode}");
    }

    logger.LogInformation(
      "Starting the fake process with command: {Command}",
      $"{finalOptions.Command} {string.Join(' ', finalOptions.Arguments)}"
    );

    var managerLogger = services.GetRequiredService<ILogger<FakeManager>>();

    return new FakeManager(managerLogger, options.Value.Fake, process);
  }

  private static string IntervalToArgument(FakeInterval interval)
  {
    return interval switch
    {
      FakeInterval.Hour => "hour",
      FakeInterval.Day => "day",
      FakeInterval.Week => "week",
      FakeInterval.Month => "month",
      FakeInterval.Season => "season",
      FakeInterval.Year => "year",
      _ => throw new ArgumentOutOfRangeException(
        nameof(interval), interval, null)
    };
  }
}
