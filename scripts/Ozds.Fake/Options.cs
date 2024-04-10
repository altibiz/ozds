using CommandLine;

namespace Ozds.Fake;

public class Options
{
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

  [Option('i', "interval", Required = false, HelpText = "Interval in seconds.")]
  public int Interval_s { get; set; } = 60;


  public static Options Parse(string[] args)
  {
    return Parser.Default.ParseArguments<Options>(args).Value;
  }
}
