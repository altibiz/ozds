using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Ozds.Client.Extensions;

namespace Ozds.Client.Components.Models.Base;

public abstract class ListModelComponent<TPrefix, TModel> : ModelComponent
{
  private Func<TPrefix, TModel?>? raw;

  [Parameter]
  public IEnumerable<TPrefix> Models { get; set; } = default!;

  [Parameter]
  public Expression<Func<TPrefix, TModel?>>? Prefix { get; set; }

  protected Func<TPrefix, TModel?> Raw
  {
    get { return raw ??= CreateRaw(); }
  }

  protected virtual Func<TPrefix, TModel?> CreateRaw()
  {
    var prefix = Prefix?.Compile() ?? (x => (TModel?)(object?)x);
    return x =>
    {
      try
      {
        return prefix(x);
      }
      catch (Exception)
      {
        return default;
      }
    };
  }

  protected override Type CreateModelType()
  {
    var prefix = Raw;
    var modelType = Models
      .Select(prefix)
      .Select(x => x?.GetType())
      .OfType<Type>()
      .CommonType();
    return modelType ?? typeof(TModel);
  }

  protected override Type CreateComponentType()
  {
    return Provider.GetPrefixedComponentType(
      typeof(TPrefix),
      ModelType,
      ModelType,
      ComponentKind
    );
  }

  protected override Dictionary<string, object> CreateParameters()
  {
    return new Dictionary<string, object>
    {
      { nameof(OzdsListModelComponentBase<object, object>.Models), Models! },
      { nameof(OzdsListModelComponentBase<object, object>.Prefix), Prefix! }
    };
  }

  protected override void OnParametersSet()
  {
    base.OnParametersSet();

    if (raw is not null)
    {
      raw = CreateRaw();
    }
  }
}
