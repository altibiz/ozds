using Bogus;
using Ozds.Fake.Faking.Abstractions;

namespace Ozds.Fake.Faking.Base;

public abstract class InheritingModelFaker<TModel, TSuperModel>(
  IServiceProvider serviceProvider
) : ConcreteModelFaker<TModel>(serviceProvider)
  where TModel : notnull, TSuperModel
  where TSuperModel : notnull
{
  private InitializingModelFaker? _baseModelFaker;

  private InitializingModelFaker BaseModelFaker
  {
    get
    {
      _baseModelFaker ??=
        serviceProvider
          .GetServices<IModelFaker>()
          .FirstOrDefault(x => x.ModelType == typeof(TSuperModel))
          as InitializingModelFaker
        ?? throw new InvalidOperationException(
          $"No model activator found for type {typeof(TSuperModel)}"
        );

      return _baseModelFaker;
    }
  }

  public override void Initialize(TModel model, Faker faker)
  {
    base.Initialize(model, faker);
    BaseModelFaker.Initialize(model, faker);
  }

  public override Faker Customize(Faker faker)
  {
    base.Customize(faker);
    return BaseModelFaker.Customize(faker);
  }
}
