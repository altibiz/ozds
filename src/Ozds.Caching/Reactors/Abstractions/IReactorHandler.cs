namespace Ozds.Caching.Reactors.Abstractions;

public interface IReactorHandler { }

public interface IReactorHandler<TEventArgs> : IReactorHandler
  where TEventArgs : EventArgs
{
  public Task AfterStartAsync(CancellationToken cancellationToken);

  public Task Handle(TEventArgs eventArgs, CancellationToken cancellationToken);

  public Task BeforeStopAsync(CancellationToken cancellationToken);
}
