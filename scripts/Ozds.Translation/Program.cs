using Ozds.Translation.Arguments;
using Ozds.Translation.Hosting;

var arguments = OzdsTranslationArguments.Parse(args);
if (arguments is null)
{
  return 1;
}

var host = new OzdsTranslationHost(arguments);

await host.RunAsync();

return 0;
