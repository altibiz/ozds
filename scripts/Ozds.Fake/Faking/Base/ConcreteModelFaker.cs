using Bogus;
using Ozds.Business.Activation;

namespace Ozds.Fake.Faking.Base;

public abstract class ConcreteModelFaker<TModel>(
  IServiceProvider serviceProvider
) : InitializingModelFaker
  where TModel : notnull
{
#pragma warning disable SA1401 // Fields should be private
  protected readonly IServiceProvider serviceProvider = serviceProvider;
#pragma warning restore SA1401 // Fields should be private

  public override Type ModelType
  {
    get { return typeof(TModel); }
  }

  public virtual void Initialize(TModel model, Faker faker) { }

  public override object Fake()
  {
    var model = Create();
    var faker = Customize(new Faker());
    Initialize(model, faker);
    return model;
  }

  public override bool CanFake(Type type)
  {
    return ModelType.IsAssignableTo(type);
  }

  public override object Box()
  {
    return Create();
  }

  public override void Initialize(object model, Faker faker)
  {
    base.Initialize(model, faker);
    Initialize((TModel)model, faker);
  }

  public virtual TModel Create()
  {
    var activator = serviceProvider.GetRequiredService<ModelActivator>();

    return activator.Activate<TModel>();
  }
}
