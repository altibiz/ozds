using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Jobs.Observers.Abstractions;

public interface IMeterJobSubscriber : ISubscriber<
  IMeterJobPublisher,
  MeterJobEventArgs>
{
}
