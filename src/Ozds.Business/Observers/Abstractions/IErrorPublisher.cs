using ErrorEventArgs = Ozds.Business.Observers.EventArgs.ErrorEventArgs;

namespace Ozds.Business.Observers.Abstractions;

public interface IErrorPublisher : IPublisher<IErrorSubscriber>
{
  public void PublishError(ErrorEventArgs eventArgs);
}