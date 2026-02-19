using Ozds.Fake.Packing.Abstractions;
using Ozds.Iot.Entities;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Fake.Packing.Implementations;

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
    var entity = new PidgeonMessengerPushRequestEntity
    {
      Timestamp = timestamp,
      Measurements = requests
        .OfType<IPidgeonMeterPushRequestEntity>()
        .ToArray(),
    };

    return entity;
  }

  public async Task<IMessengerPushRequestEntity> Pack(
    string messengerId,
    DateTimeOffset timestamp,
    IAsyncEnumerable<IMeterPushRequestEntity> requests,
    CancellationToken cancellationToken
  )
  {
    var entity = new PidgeonMessengerPushRequestEntity
    {
      Timestamp = timestamp,
      Measurements = await requests
        .OfType<IPidgeonMeterPushRequestEntity>()
        .ToArrayAsync(cancellationToken),
    };

    return entity;
  }
}
