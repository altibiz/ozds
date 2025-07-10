namespace Ozds.Fake.Faking.Abstractions;

public interface IModelFaker
{
  public Type ModelType { get; }

  public bool CanFake(Type type);

  public object Fake();
}
