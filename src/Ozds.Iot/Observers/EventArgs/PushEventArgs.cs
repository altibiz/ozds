using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Iot.Observers.EventArgs;

public enum PushEventBufferBehavior
{
  Realtime,
  Buffer
}

public class PushEventArgs : System.EventArgs
{
  public required string MessengerId { get; init; }

  public required PushEventBufferBehavior BufferBehavior { get; init; }

  public required IMessengerPushRequestEntity Request { get; init; }
}
