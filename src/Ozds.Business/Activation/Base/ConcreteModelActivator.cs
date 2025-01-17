namespace Ozds.Business.Activation.Base;

public abstract class ConcreteModelActivator<TModel> : InitializingModelActivator
  where TModel : notnull
{
  public virtual void Initialize(TModel model)
  {
  }

  public override Type ModelType
  {
    get { return typeof(TModel); }
  }

  public override bool CanActivate(Type type)
  {
    return ModelType.IsAssignableTo(type);
  }

  public override object Box()
  {
    return Create();
  }

  public override void Initialize(object model)
  {
    base.Initialize(model);
    Initialize((TModel)model);
  }

  public virtual TModel Create()
  {
    return Activator.CreateInstance<TModel>();
  }
}
