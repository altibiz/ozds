using CommandLine;

namespace Ozds.Fake.Arguments;

public enum OzdsFakeIntervalArgument
{
  Hour,
  Day,
  Week,
  Month,
  Season,
  Year
}

public static class OzdsFakeIntervalOptionExtensions
{
  public static TimeSpan ToTimeSpan(this OzdsFakeIntervalArgument interval)
  {
    return interval switch
    {
      OzdsFakeIntervalArgument.Hour => TimeSpan.FromHours(1),
      OzdsFakeIntervalArgument.Day => TimeSpan.FromDays(1),
      OzdsFakeIntervalArgument.Week => TimeSpan.FromDays(7),
      OzdsFakeIntervalArgument.Month => TimeSpan.FromDays(30),
      OzdsFakeIntervalArgument.Season => TimeSpan.FromDays(90),
      OzdsFakeIntervalArgument.Year => TimeSpan.FromDays(365),
      _ => throw new InvalidOperationException($"Unknown interval: {interval}")
    };
  }
}

[Verb("push", HelpText = "Push measurements to the API.")]
public class OzdsFakePushArguments
{
  [Option('m', "messenger-id", Required = false, HelpText = "Messenger ID.")]
  public string MessengerId { get; set; } = "pidgeon";

  [Option('e', "meter-ids", Required = false, HelpText = "Meter IDs.")]
  public IEnumerable<string> MeterIds { get; set; } = [];

  [Option(
    'l', "location-id", Required = false,
    HelpText = "Location ID. If not specified, all meters will be used.")]
  public string? LocationId { get; set; } = default!;

  [Option('t', "timeout", Required = false, HelpText = "Timeout in seconds.")]
  public int Timeout_s { get; set; } = 3;

  [Option('i', "interval", Required = false, HelpText = "Interval in seconds.")]
  public int Interval_s { get; set; } = 5;
}

[Verb("seed", HelpText = "Seed the database via OZDS with a desired interval.")]
public class OzdsFakeSeedArguments
{
  [Option('i', "interval", Required = true, HelpText = "Desired interval.")]
  public OzdsFakeIntervalArgument Interval { get; set; } =
    OzdsFakeIntervalArgument.Month;

  [Option('b', "batch-size", Required = false, HelpText = "Batch size.")]
  public int BatchSize { get; set; } = 10000;

  [Option('m', "messenger-id", Required = false, HelpText = "Messenger ID.")]
  public string MessengerId { get; set; } = "pidgeon";

  [Option('e', "meter-ids", Required = false, HelpText = "Meter IDs.")]
  public IEnumerable<string> MeterIds { get; set; } = [];

  [Option(
    'l', "location-id", Required = false,
    HelpText = "Location ID. If not specified, all meters will be used.")]
  public string? LocationId { get; set; } = default!;

  [Option('t', "timeout", Required = false, HelpText = "Timeout in seconds.")]
  public int Timeout_s { get; set; } = 3;
}

[Verb(
  "insert",
  HelpText =
    "Seed the database by directly inserting with a desired interval.")]
public class OzdsFakeInsertArguments
{
  [Option('i', "interval", Required = true, HelpText = "Desired interval.")]
  public OzdsFakeIntervalArgument Interval { get; set; } =
    OzdsFakeIntervalArgument.Month;

  // NOTE: smaller batch size here because
  // it usually times out on 10k with aggregates only
  [Option('b', "batch-size", Required = false, HelpText = "Batch size.")]
  public int BatchSize { get; set; } = 1000;

  [Option(
    'e', "meters", Required = false, Min = 1,
    HelpText =
      "Meters in form of `<measurement-location-id>:<meter-id>`. If not specified, location will be used to fetch meters in that location.")]
  public IEnumerable<string> Meters { get; set; } = [];

  [Option(
    'l', "location-id", Required = false,
    HelpText = "Location ID. If not specified, all meters will be used.")]
  public string? LocationId { get; set; } = default!;

  [Option('t', "timeout", Required = false, HelpText = "Timeout in seconds.")]
  public int Timeout_s { get; set; } = 3;

  [Option(
    'a', "aggregates-only", Required = false,
    HelpText = "Only generate aggregates.")]
  public bool AggregatesOnly { get; set; } = false;
}

[Verb("altibiz", HelpText = "Fake Altibiz ERP web application.")]
public class OzdsFakeAltibizArguments
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

public static class OzdsFakeArguments
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
        }).ParseArguments<OzdsFakePushArguments, OzdsFakeSeedArguments,
        OzdsFakeInsertArguments,
        OzdsFakeAltibizArguments>(args);

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
