namespace Ozds.Fake.Loaders.Abstractions;

public interface ILoader<T> where T : class
{
  T Load(Stream stream);
}
