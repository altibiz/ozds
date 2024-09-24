using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Iot.Observers.EventArgs;

public class PushEventArgs(
  string messengerId,
  IMessengerPushRequestEntity request)
  : System.EventArgs
{
  public string MessengerId { get; } = messengerId;

  public IMessengerPushRequestEntity Request { get; } = request;
}
