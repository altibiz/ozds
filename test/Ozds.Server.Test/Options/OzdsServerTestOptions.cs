using Microsoft.Extensions.Options;

namespace Ozds.Server.Test.Options;

public class OzdsServerTestOptions
{
  public string StatusUri { get; set; } = default!;

  public int Timeout_s { get; set; } = default!;

  public OzdsServerTestStartupOptions? Startup { get; set; } = default!;

  public OzdsServerTestFakeOptions Fake { get; set; } = default!;
}

public class OzdsServerTestStartupOptions
{
  public string Command { get; set; } = default!;

  public List<string> Arguments { get; set; } = default!;

  public Dictionary<string, string> Environment { get; set; } = default!;

  public string WorkingDirectory { get; set; } = default!;
}

public class OzdsServerTestFakeOptions
{
  public string Command { get; set; } = default!;

  public List<string> Arguments { get; set; } = default!;

  public Dictionary<string, string> Environment { get; set; } = default!;

  public string WorkingDirectory { get; set; } = default!;
}

public class OzdsServerTestConfigureOptions(IConfiguration configuration)
  : IConfigureOptions<OzdsServerTestOptions>
{
  public void Configure(OzdsServerTestOptions options)
  {
    configuration.GetSection("Ozds:Server:Test").Bind(options);
  }
}
