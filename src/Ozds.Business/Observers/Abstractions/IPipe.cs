namespace Ozds.Business.Observers.Abstractions;

public interface IPipe
{
}

public interface IPipe<TInEventArgs, TOutEventArgs> : IPipe
  where TInEventArgs : System.EventArgs
  where TOutEventArgs : System.EventArgs
{
  public Task<TOutEventArgs> Transform(
    TInEventArgs eventArgs,
    CancellationToken cancellationToken
  );
}
