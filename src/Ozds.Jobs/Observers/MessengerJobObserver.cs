using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.Base;
using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Jobs.Observers;

public class MessengerJobManager :
  Observer<MessengerJobEventArgs>,
  IMessengerJobPublisher,
  IMessengerJobSubscriber
{
}
