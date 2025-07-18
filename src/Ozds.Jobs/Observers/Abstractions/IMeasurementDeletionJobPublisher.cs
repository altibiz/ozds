using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Jobs.Observers.Abstractions;

public interface IArchivalJobPublisher : IPublisher<
  IArchivalJobSubscriber,
  ArchivalJobEventArgs>
{
}
