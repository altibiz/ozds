using Ozds.Migration.Arguments;
using Ozds.Migration.Hosting;

var arguments = OzdsMigrationArguments.Parse(args);
if (arguments is null)
{
  return 1;
}

var host = new OzdsMigrationHost(arguments);

await host.RunAsync();

return 0;
