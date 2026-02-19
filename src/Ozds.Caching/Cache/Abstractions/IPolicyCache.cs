namespace Ozds.Caching.Cache.Abstractions;

public interface IPolicyCache
{
  public Task Create(
    string key,
    object value,
    CancellationToken cancellationToken
  );

  public Task<object?> Read(
    Type type,
    string key,
    CancellationToken cancellationToken
  );

  public Task Delete(string key, CancellationToken cancellationToken);
}
