using Ozds.Fake.Packing.Abstractions;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Fake.Packing.Agnostic;

public class AgnosticMessengerPushRequestPacker(IServiceProvider services)
{
  public IMessengerPushRequestEntity Pack(
    string messengerId,
    DateTimeOffset timestamp,
    IEnumerable<IMeterPushRequestEntity> requests
  )
  {
    var packer = services.GetServices<IMessengerPushRequestPacker>()
        .FirstOrDefault(p => p.CanPack(messengerId))
      ?? throw new InvalidOperationException(
        $"No packer found for {messengerId}");

    return packer.Pack(messengerId, timestamp, requests);
  }
}
