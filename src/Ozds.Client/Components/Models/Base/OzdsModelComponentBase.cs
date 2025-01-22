using Ozds.Client.Components.Base;
using Ozds.Client.Components.Models.Abstractions;

namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsModelComponentBase<TModel> : OzdsComponentBase
{
  public abstract class ModelComponentProviderBase : IModelComponentProvider
  {
    public Type ModelType => typeof(TModel);

    public bool CanRender(Type type)
    {
      return type.IsAssignableTo(ModelType);
    }

    public abstract Type ComponentType { get; }

    public abstract ModelComponentKind ComponentKind { get; }

    protected Type FindComponentType(Type baseType)
    {
      return typeof(OzdsModelComponentBase<>).Assembly
        .GetTypes()
        .First(type =>
          !type.IsGenericType
          && !type.IsAbstract
          && type.IsClass
          && type.IsAssignableTo(baseType));
    }
  }
}
