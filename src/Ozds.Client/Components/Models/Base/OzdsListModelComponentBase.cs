using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsListModelComponentBase<TPrefix, TModel> :
  OzdsPrefixedModelComponentBase<TPrefix, TModel>
{
  private Func<TPrefix, TModel?>? raw;

  [Parameter]
  public IEnumerable<TPrefix> Models { get; set; } = default!;

  protected Func<TPrefix, TModel?> Raw
  {
    get { return raw ??= CreateRaw(); }
  }

  protected Func<TPrefix, T?> Get<T>(
    Func<TModel, T?> next
  )
  {
    var first = Raw;
    return x => first(x) is { } y ? next(y) : default;
  }

  protected virtual Func<TPrefix, TModel?> CreateRaw()
  {
    var prefix = Prefix?.Compile() ?? (x => (TModel?)(object?)x);
    return prefix;
  }

  protected override Dictionary<string, object> CreateBaseParameters()
  {
    return new Dictionary<string, object>
    {
      { nameof(Models), Models! },
      { nameof(Prefix), Prefix! }
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
