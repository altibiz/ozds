using Ozds.Business.Iot;
using Ozds.Fake;
using Ozds.Fake.Client;
using Ozds.Fake.Extensions;
using Ozds.Fake.Generators.Abstractions;

var options = Options.Parse(args);

var serviceCollection = new ServiceCollection();

serviceCollection.AddLoaders();
serviceCollection.AddGenerators();
serviceCollection.AddClient();

#pragma warning disable ASP0000
var serviceProvider = serviceCollection.BuildServiceProvider();
#pragma warning restore ASP0000

while (true)
{
  var now = DateTimeOffset.UtcNow;
  var lastMinute = now.AddMinutes(-1);
  var pushClient = serviceProvider.GetRequiredService<OzdsPushClient>();
  var generators = serviceProvider.GetServices<IMeasurementGenerator>();

  var measurements = new List<MessengerPushRequestMeasurement>();
  foreach (var meterId in options.MeterIds)
  {
    foreach (var generator in generators)
    {
      if (generator.CanGenerateMeasurementsFor(meterId))
      {
        measurements.AddRange(await generator
          .GenerateMeasurements(lastMinute, now, meterId));
      }
    }
  }

  var request = new MessengerPushRequest(
    now,
    measurements.ToArray()
  );

  await pushClient.Push(
    options.BaseUrl,
    options.MessengerId,
    options.ApiKey,
    request
  );

  await Task.Delay(1000 * 60);
}
