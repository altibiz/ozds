using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsListModelComponentBase<TPrefix, TModel> :
  OzdsPrefixedModelComponentBase<TPrefix, TModel>
{
  [Parameter]
  public IEnumerable<TPrefix> Models { get; set; } = default!;

  protected Func<TPrefix, T?> Get<T>(
    Func<TModel, T?> next
  )
  {
    var first = Raw;
    return x => first(x) is { } y ? next(y) : default;
  }

  protected Expression<Func<TPrefix, T?>> Wrap<T>(
    Expression<Func<TModel, T?>> next
  )
  {
    var first = Exp;
    var param = first.Parameters[0];
    var modelExpression = first.Body;
    var nullCheck = Expression.Equal(
      modelExpression,
      Expression.Constant(null, typeof(TModel))
    );
    var ifTrue = Expression.Constant(default(T), typeof(T));
    var ifFalse = Expression.Invoke(next, modelExpression);
    var body = Expression.Condition(nullCheck, ifTrue, ifFalse);
    return Expression.Lambda<Func<TPrefix, T?>>(body, param);
  }

  protected Expression<Func<TPrefix, T>> Member<T>(
    Expression<Func<TModel, T>> next
  )
  {
    var first = Exp;
    var prefixParam = first.Parameters[0];
    var replacedBody = ParameterReplacer.Replace(
      next.Body,
      next.Parameters[0],
      first.Body
    );
    return Expression.Lambda<Func<TPrefix, T>>(replacedBody, prefixParam);
  }

  protected Func<TPrefix, TModel?> Raw =>
    raw ??= CreateRaw();

  private Func<TPrefix, TModel?>? raw;

  protected virtual Func<TPrefix, TModel?> CreateRaw()
  {
    return Prefix?.Compile() ?? ((TPrefix x) => (TModel?)(object?)x);
  }

  protected Expression<Func<TPrefix, TModel?>> Exp =>
    exp ??= CreateExp();

  private Expression<Func<TPrefix, TModel?>>? exp;

  protected virtual Expression<Func<TPrefix, TModel?>> CreateExp()
  {
    return Prefix ?? ((TPrefix x) => (TModel?)(object?)x);
  }

  protected override Dictionary<string, object> CreateBaseParameters()
  {
    return new()
    {
      { nameof(Models), Models! },
      { nameof(Prefix), Prefix! }
    };
  }

  protected override void OnParametersSet()
  {
    base.OnParametersSet();

    if (raw is { })
    {
      raw = CreateRaw();
    }

    if (exp is { })
    {
      exp = CreateExp();
    }
  }
}
