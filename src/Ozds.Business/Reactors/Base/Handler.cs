using Ozds.Business.Reactors.Abstractions;

namespace Ozds.Business.Reactors.Base;

public abstract class Handler<TEventArgs> : IHandler<TEventArgs>
  where TEventArgs : EventArgs
{
  public virtual Task AfterStartAsync(CancellationToken cancellationToken)
  {
    return Task.CompletedTask;
  }

  public abstract Task Handle(
    TEventArgs eventArgs,
    CancellationToken cancellationToken);

  public virtual Task BeforeStopAsync(CancellationToken cancellationToken)
  {
    return Task.CompletedTask;
  }
}
