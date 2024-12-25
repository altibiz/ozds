using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Iot.Observers.EventArgs;

public enum PushEventBufferBehavior
{
  Realtime,
  Buffer
}

public class PushEventArgs(
  string messengerId,
  PushEventBufferBehavior bufferBehavior,
  IMessengerPushRequestEntity request)
  : System.EventArgs
{
  public string MessengerId { get; } = messengerId;

  public PushEventBufferBehavior BufferBehavior { get; } = bufferBehavior;

  public IMessengerPushRequestEntity Request { get; } = request;
}
