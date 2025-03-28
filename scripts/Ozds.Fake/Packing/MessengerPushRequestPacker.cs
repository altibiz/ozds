using Ozds.Fake.Packing.Abstractions;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Fake.Packing;

public class MessengerPushRequestPacker(IServiceProvider services)
{
  public IMessengerPushRequestEntity Pack(
    string messengerId,
    DateTimeOffset timestamp,
    IEnumerable<IMeterPushRequestEntity> requests
  )
  {
    var packer = GetPacker(messengerId);
    return packer.Pack(messengerId, timestamp, requests);
  }

  public async Task<IMessengerPushRequestEntity> Pack(
    string messengerId,
    DateTimeOffset timestamp,
    IAsyncEnumerable<IMeterPushRequestEntity> requests,
    CancellationToken cancellationToken
  )
  {
    var packer = GetPacker(messengerId);
    return await packer.Pack(
      messengerId,
      timestamp,
      requests,
      cancellationToken
    );
  }

  private IMessengerPushRequestPacker GetPacker(
    string messengerId
  )
  {
    return services.GetServices<IMessengerPushRequestPacker>()
        .FirstOrDefault(p => p.CanPack(messengerId))
      ?? throw new InvalidOperationException(
        $"No packer found for {messengerId}");
  }
}
