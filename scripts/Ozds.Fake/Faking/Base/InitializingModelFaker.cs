using Bogus;
using Ozds.Fake.Faking.Abstractions;

namespace Ozds.Fake.Faking.Base;

public abstract class InitializingModelFaker : IModelFaker
{
  public abstract Type ModelType { get; }

  public abstract bool CanFake(Type type);

  public virtual object Fake()
  {
    var model = Box();
    var faker = Customize(new Faker());
    Initialize(model, faker);
    return model;
  }

  public abstract object Box();

  public virtual Faker Customize(Faker faker)
  {
    return faker;
  }

  public virtual void Initialize(object model, Faker faker) { }
}
