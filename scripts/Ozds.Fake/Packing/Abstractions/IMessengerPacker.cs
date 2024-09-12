using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Fake.Packing.Abstractions;

public interface IMessengerPacker
{
  public IMessengerPushRequestEntity Pack(
    string messengerId,
    DateTimeOffset timestamp,
    IEnumerable<IMeterPushRequestEntity> requests
  );
}
