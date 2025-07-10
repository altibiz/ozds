using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.Base;
using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Jobs.Observers.Implementations;

public class ArchivalJobObserver :
  Observer<ArchivalJobEventArgs>,
  IArchivalJobPublisher,
  IArchivalJobSubscriber
{
}
