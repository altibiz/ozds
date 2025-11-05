using Ozds.Business.Hosting;
using Ozds.Translation.Arguments;
using Ozds.Translation.Extensions;

namespace Ozds.Translation.Hosting;

public sealed class OzdsTranslationHost(
  IOzdsTranslationArguments arguments
) : OzdsBusinessHost<
  HostApplicationBuilder,
  IHost>(
    Host.CreateApplicationBuilder(),
    builder =>
    {
      builder.Configuration.AddInMemoryCollection(
        new Dictionary<string, string?>
        {
          { "Ozds:Users:WithAuth", "false" },
          { "Ozds:Messaging:WithBus", "false" },
          { "Ozds:Messaging:WithServices", "false" },
          { "Ozds:Jobs:WithServices", "false" },
          { "Ozds:Business:WithReactors", "false" },
          { "Ozds:Data:WithServices", "false" }
        });

      builder.AddOzdsTranslation(arguments);
    },
    builder =>
    {
      var inner = builder.Build();

      return inner;
    }
  )
{
}
