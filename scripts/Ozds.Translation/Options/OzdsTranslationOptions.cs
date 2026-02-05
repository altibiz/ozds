using Microsoft.Extensions.Options;

namespace Ozds.Translation.Options;

public class OzdsTranslationOptions
{
  public OzdsTranslationOpenAiApiOptions OpenAiApi { get; set; } = default!;
}

public class OzdsTranslationOpenAiApiOptions
{
  public string BaseUrl { get; set; } = default!;

  public string ApiKey { get; set; } = default!;
}

public class ConfigureOzdsTranslationOptions(IConfiguration configuration)
  : IConfigureOptions<OzdsTranslationOptions>
{
  public void Configure(OzdsTranslationOptions options)
  {
    configuration.GetSection("Ozds:Translation").Bind(options);
  }
}
