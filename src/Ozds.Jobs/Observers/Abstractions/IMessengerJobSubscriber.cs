using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Jobs.Observers.Abstractions;

public interface IMessengerJobSubscriber : ISubscriber<
  IMessengerJobPublisher,
  MessengerJobEventArgs>
{
}
