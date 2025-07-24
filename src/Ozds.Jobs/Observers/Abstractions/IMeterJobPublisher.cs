using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Jobs.Observers.Abstractions;

public interface IMeterJobPublisher : IPublisher<
  IMeterJobSubscriber,
  MeterJobEventArgs>
{
}
