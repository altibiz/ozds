using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Fake.Packing.Abstractions;

public interface IMessengerPushRequestPacker
{
  public bool CanPack(string messengerId);

  public IMessengerPushRequestEntity Pack(
    string messengerId,
    DateTimeOffset timestamp,
    IEnumerable<IMeterPushRequestEntity> requests
  );
}
