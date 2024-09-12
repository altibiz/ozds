using CommandLine;

namespace Ozds.Fake;

public enum SeedInterval
{
  Hour,
  Day,
  Week,
  Month,
  Season,
  Year
}

[Verb("push", HelpText = "Push measurements to the API.")]
public class PushOptions
{
  [Option('b', "base-url", Required = false, HelpText = "Base URL of the API.")]
  public string BaseUrl { get; set; } = "http://localhost:5000";

  [Option('a', "api-key", Required = false, HelpText = "API key.")]
  public string ApiKey { get; set; } = "messenger";

  [Option('m', "messenger-id", Required = false, HelpText = "Messenger ID.")]
  public string MessengerId { get; set; } = "pidgeon";

  [Option('i', "meter-ids", Required = false, HelpText = "Meter IDs.")]
  public IEnumerable<string> MeterIds { get; set; } = Array.Empty<string>();

  [Option('t', "timeout", Required = false, HelpText = "Timeout in seconds.")]
  public int Timeout_s { get; set; } = 3;

  [Option('n', "interval", Required = false, HelpText = "Interval in seconds.")]
  public int Interval_s { get; set; } = 60;
}

[Verb("seed", HelpText = "Seed the database with a desired interval.")]
public class SeedOptions
{
  [Option('i', "interval", Required = false, HelpText = "Desired interval.")]
  public SeedInterval Interval { get; set; } = SeedInterval.Hour;

  [Option('b', "batch-size", Required = false, HelpText = "Batch size.")]
  public int BatchSize { get; set; } = 10000;

  [Option('b', "base-url", Required = false, HelpText = "Base URL of the API.")]
  public string BaseUrl { get; set; } = "http://localhost:5000";

  [Option('a', "api-key", Required = false, HelpText = "API key.")]
  public string ApiKey { get; set; } = "messenger";

  [Option('m', "messenger-id", Required = false, HelpText = "Messenger ID.")]
  public string MessengerId { get; set; } = "messenger";

  [Option('i', "meter-ids", Required = false, HelpText = "Meter IDs.")]
  public IEnumerable<string> MeterIds { get; set; } = Array.Empty<string>();

  [Option('t', "timeout", Required = false, HelpText = "Timeout in seconds.")]
  public int Timeout_s { get; set; } = 3;
}

[Verb("altibiz", HelpText = "Fake Altibiz ERP web application.")]
public class AltibizOptions
{
  [Option('o', "host", Required = false, HelpText = "RabbitMQ host.")]
  public string Host { get; set; } = "localhost";

  [Option(
    'i', "virtual-host", Required = false,
    HelpText = "RabbitMQ virtual host.")]
  public string VirtualHost { get; set; } = "/";

  [Option('u', "username", Required = false, HelpText = "RabbitMQ username.")]
  public string Username { get; set; } = "ozds";

  [Option('p', "password", Required = false, HelpText = "RabbitMQ password.")]
  public string Password { get; set; } = "ozds";
}

public static class Options
{
  public static object? Parse(string[] args)
  {
    try
    {
      var result = new Parser(
        with =>
        {
          with.CaseInsensitiveEnumValues = true;
          with.AutoHelp = true;
          with.AutoVersion = true;
          with.HelpWriter = Console.Out;
        }).ParseArguments<PushOptions, SeedOptions, AltibizOptions>(args);

      if (result.Tag == ParserResultType.NotParsed)
      {
        if (result.Errors.Any(e => !e.StopsProcessing))
        {
          foreach (var error in result.Errors)
          {
            Console.Error.WriteLine(error);
          }

          Environment.Exit(1);
        }
        else
        {
          Environment.Exit(0);
        }
      }

      return result.Value;
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine(ex.Message);
      Environment.Exit(1);
    }

    return default;
  }
}
