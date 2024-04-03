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

  [Option('i', "meter-id", Required = false, HelpText = "Meter IDs.")]
  public IEnumerable<string> MeterIds { get; set; } = Array.Empty<string>();


  public static Options Parse(string[] args)
  {
    return Parser.Default.ParseArguments<Options>(args).Value;
  }
}
