namespace Ozds.Business.Reactors.Abstractions;

public interface IHandler
{
}

public interface IHandler<TEventArgs> : IHandler
  where TEventArgs : EventArgs
{
  public Task AfterStartAsync(CancellationToken cancellationToken);

  public Task Handle(
    TEventArgs eventArgs,
    CancellationToken cancellationToken);

  public Task BeforeStopAsync(CancellationToken cancellationToken);
}
