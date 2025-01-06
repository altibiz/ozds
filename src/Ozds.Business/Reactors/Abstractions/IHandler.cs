namespace Ozds.Business.Reactors.Abstractions;

public interface IHandler
{
}

public interface IHandler<TEventArgs> : IHandler
  where TEventArgs : EventArgs
{
  public Task Handle(TEventArgs eventArgs, CancellationToken cancellationToken);
}
