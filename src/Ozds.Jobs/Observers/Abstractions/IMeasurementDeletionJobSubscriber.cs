using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Jobs.Observers.Abstractions;

public interface IArchivalJobSubscriber : ISubscriber<
  IArchivalJobPublisher,
  ArchivalJobEventArgs>
{
}
