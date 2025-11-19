using Ozds.Business.Hosting;
using Ozds.Fake.Arguments;
using Ozds.Fake.Extensions;

namespace Ozds.Fake.Hosting;

public sealed class OzdsFakeHost(
  IOzdsFakeArguments arguments,
  HostApplicationBuilderSettings? settings = null,
  Action<HostApplicationBuilder>? configure = null
) : OzdsBusinessHost<
  HostApplicationBuilder,
  IHost>(
  settings is null
    ? Host.CreateApplicationBuilder()
    : Host.CreateApplicationBuilder(settings),
  builder =>
  {
    if (configure is not null)
    {
      configure(builder);
    }

    builder.Configuration.AddInMemoryCollection(
      new Dictionary<string, string?>
      {
        { "Ozds:Users:WithAuth", "false" },
        { "Ozds:Messaging:WithBus", "false" },
        { "Ozds:Messaging:WithServices", "false" },
        { "Ozds:Jobs:WithServices", "false" },
        { "Ozds:Business:WithReactors", "false" }
      });

    if (arguments is not OzdsFakeInsertArguments)
    {
      builder.Configuration.AddInMemoryCollection(
        new Dictionary<string, string?>
        {
          { "Ozds:Data:WithServices", "false" }
        }
      );
    }

    builder.AddOzdsFake(arguments);
  },
  builder =>
  {
    var inner = builder.Build();

    return inner;
  }
)
{
}
