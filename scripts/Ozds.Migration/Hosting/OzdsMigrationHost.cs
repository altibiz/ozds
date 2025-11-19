using Ozds.Business.Hosting;
using Ozds.Migration.Arguments;
using Ozds.Migration.Extensions;

namespace Ozds.Migration.Hosting;

public sealed class OzdsMigrationHost(
  IOzdsMigrationArguments arguments
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

    builder.AddOzdsMigration(arguments);
  },
  builder =>
  {
    var inner = builder.Build();

    return inner;
  }
)
{
}
