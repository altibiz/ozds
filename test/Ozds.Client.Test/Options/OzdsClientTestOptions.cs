using Microsoft.Extensions.Options;

namespace Ozds.Client.Test.Options;

public class OzdsClientTestOptions
{
  public OzdsClientTestServerOptions Server { get; set; } = default!;

  public OzdsClientTestBrowserOptions Browser { get; set; } = default!;
}

public class OzdsClientTestBrowserOptions
{
  public string BaseUri { get; set; } = default!;
}

public class OzdsClientTestServerOptions
{
  public string StatusUri { get; set; } = default!;

  public int Timeout_s { get; set; } = default!;

  public OzdsClientTestServerStartupOptions? Startup { get; set; } = default!;
}

public class OzdsClientTestServerStartupOptions
{
  public string Command { get; set; } = default!;

  public List<string> Arguments { get; set; } = default!;

  public Dictionary<string, string> Environment { get; set; } = default!;

  public string WorkingDirectory { get; set; } = default!;
}

public class OzdsClientTestConfigureOptions(IConfiguration configuration)
  : IConfigureOptions<OzdsClientTestOptions>
{
  public void Configure(OzdsClientTestOptions options)
  {
    configuration.GetSection("Ozds:Client:Test").Bind(options);
  }
}
