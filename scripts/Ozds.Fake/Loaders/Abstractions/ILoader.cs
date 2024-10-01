namespace Ozds.Fake.Loaders.Abstractions;

public interface ILoader
{
}

public interface ILoader<T> : ILoader
  where T : class
{
  T Load(Stream stream);
}
