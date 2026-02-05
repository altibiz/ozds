namespace Ozds.Fake.Loading.Abstractions;

public interface ILoader { }

public interface ILoader<T> : ILoader
  where T : class
{
  T Load(Stream stream);

  Task<T> Load(Stream stream, CancellationToken cancellationToken);
}
