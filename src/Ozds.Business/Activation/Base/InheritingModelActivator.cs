using Ozds.Business.Activation.Abstractions;

namespace Ozds.Business.Activation.Base;

public abstract class InheritingModelActivator<TModel, TSuperModel>(
  IServiceProvider serviceProvider
) : ConcreteModelActivator<TModel>
  where TModel : notnull, TSuperModel
  where TSuperModel : notnull
{
  private readonly InitializingModelActivator _baseModelActivator =
    serviceProvider
      .GetServices<IModelActivator>()
      .FirstOrDefault(x => x.ModelType == typeof(TSuperModel))
      as InitializingModelActivator
        ?? throw new InvalidOperationException(
          $"No model activator found for  type {typeof(TModel).BaseType}");

  public override void Initialize(TModel model)
  {
    base.Initialize(model);
    _baseModelActivator.Initialize(model);
  }
}
