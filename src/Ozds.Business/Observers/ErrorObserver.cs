using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.Base;
using ErrorEventArgs = Ozds.Business.Observers.EventArgs.ErrorEventArgs;

namespace Ozds.Business.Observers;

public class ErrorObserver :
  Observer<ErrorEventArgs>,
  IErrorPublisher,
  IErrorSubscriber
{
}
