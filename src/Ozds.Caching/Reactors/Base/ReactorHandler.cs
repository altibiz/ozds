using Ozds.Caching.Reactors.Abstractions;

namespace Ozds.Caching.Reactors.Base;

public abstract class ReactorHandler<TEventArgs> : IReactorHandler<TEventArgs>
  where TEventArgs : EventArgs
{
  public virtual Task AfterStartAsync(CancellationToken cancellationToken)
  {
    return Task.CompletedTask;
  }

  public abstract Task Handle(
    TEventArgs eventArgs,
    CancellationToken cancellationToken
  );

  public virtual Task BeforeStopAsync(CancellationToken cancellationToken)
  {
    return Task.CompletedTask;
  }
}
