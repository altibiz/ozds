using Ozds.Fake.Packing.Abstractions;
using Ozds.Iot.Entities;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Fake.Packing;

public class PidgeonMessengerPushRequestPacker : IMessengerPushRequestPacker
{
  public bool CanPack(string messengerId)
  {
    return messengerId.StartsWith("pidgeon");
  }

  public IMessengerPushRequestEntity Pack(
    string messengerId,
    DateTimeOffset timestamp,
    IEnumerable<IMeterPushRequestEntity> requests
  )
  {
    return new PidgeonMessengerPushRequestEntity(
      timestamp,
      requests.OfType<IPidgeonMeterPushRequestEntity>().ToArray()
    );
  }
}
