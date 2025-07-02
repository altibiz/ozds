namespace Ozds.Client.Test.Containers;

public interface IComposableService
{
}

public interface IComposableService<T>
  : IComposableService, IAsyncDisposable
{
  static abstract Task<T> Create(
    ContainerNetwork network,
    CancellationToken cancellationToken
  );

  Task Configure(
    ServiceComposition composition,
    CancellationToken cancellationToken
  );

  Task Start(CancellationToken cancellationToken);

  Task Stop(CancellationToken cancellationToken);
}
