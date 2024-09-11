using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Iot.Observers.EventArgs;

public class PushEventArgs(IMessengerPushRequestEntity request)
  : System.EventArgs
{
  public IMessengerPushRequestEntity Request { get; } = request;
}
