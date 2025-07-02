using Ozds.Fake.Arguments;
using Ozds.Fake.Hosting;

var arguments = OzdsFakeArguments.Parse(args);
if (arguments is null)
{
  return 1;
}

var host = new OzdsFakeHost(arguments);

await host.RunAsync();

return 0;
