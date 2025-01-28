using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsManagedModelComponentBase<TModel> :
  OzdsModelComponentBase<TModel>
{
  [Parameter]
  public TModel Model { get; set; } = default!;

  protected override Dictionary<string, object> CreateBaseParameters()
  {
    return new()
    {
      { nameof(Model), Model! }
    };
  }
}

public abstract class OzdsManagedModelComponentBase<TPrefix, TModel> :
  OzdsPrefixedModelComponentBase<TPrefix, TModel>
{
  [Parameter]
  public TPrefix Model { get; set; } = default!;

  protected T? Get<T>(
    Func<TModel, T> next
  )
  {
    var first = Raw();
    if (first is { })
    {
      return next(first);
    }

    return default;
  }

  protected T Get<T>(
    Func<TModel, T> next,
    Func<T> @default
  )
  {
    var first = Raw();
    if (first is { })
    {
      return next(first);
    }

    return @default();
  }

  protected EventCallback<T?> Set<T>(
    Expression<Func<TModel, T>> next
  )
  {
    var parameter = Expression.Parameter(typeof(T));
    var prefixGetter = Fix;
    var nullCheck = Expression.NotEqual(
      prefixGetter,
      Expression.Constant(default(TModel))
    );
    var memberGetter = ParameterReplacer.Replace(
      next.Body,
      next.Parameters[0],
      prefixGetter.Body
    );
    Console.WriteLine(memberGetter);
    var memberAssignment = Expression.Assign(
      memberGetter,
      parameter
    );
    var body = Expression.IfThen(
      nullCheck,
      memberAssignment
    );
    var lambda = Expression.Lambda<Action<T?>>(body, parameter);
    return new(null, lambda.Compile());
  }

  protected EventCallback<TAdapter> Set<TField, TAdapter>(
    Expression<Func<TModel, TField>> next,
    Expression<Func<TAdapter, TField>> adapt
  )
  {
    var parameter = Expression.Parameter(typeof(TAdapter));
    var adapted = Expression.Invoke(adapt, parameter);
    var prefixGetter = Fix;
    var nullCheck = Expression.NotEqual(
      prefixGetter,
      Expression.Constant(default(TModel))
    );
    var memberGetter = ParameterReplacer.Replace(
      next.Body,
      next.Parameters[0],
      prefixGetter.Body
    );
    var memberAssignment = Expression.Assign(
      memberGetter,
      adapted
    );
    var body = Expression.IfThen(
      nullCheck,
      memberAssignment
    );
    var lambda = Expression.Lambda<Action<TAdapter>>(body, parameter);
    return new(null, lambda.Compile());
  }

  protected Expression<Func<T?>> For<T>(
    Expression<Func<TModel, T?>> next
  )
  {
    var first = Fix;
    var replacedBody = ParameterReplacer.Replace(
      next.Body,
      next.Parameters[0],
      first.Body
    );
    return Expression.Lambda<Func<T?>>(replacedBody);
  }

  protected Func<TModel?> Raw =>
    raw ??= CreateRaw();

  private Func<TModel?>? raw;

  protected virtual Func<TModel?> CreateRaw()
  {
    var compiled = Prefix?.Compile();
    if (compiled is null)
    {
      return () => (TModel?)(object?)Model;
    }

    return () => compiled(Model);
  }

  protected Expression<Func<TModel?>> Fix =>
    fix ??= CreateFix();

  private Expression<Func<TModel?>>? fix;

  protected virtual Expression<Func<TModel?>> CreateFix()
  {
    var prefix = Prefix;
    if (prefix is null)
    {
      return () => (TModel?)(object?)Model;
    }

    var replacedBody = ParameterReplacer.Replace(
      prefix.Body,
      prefix.Parameters[0],
      Expression.Constant(Model)
    );
    return Expression.Lambda<Func<TModel?>>(replacedBody);
  }

  protected override Dictionary<string, object> CreateBaseParameters()
  {
    return new()
    {
      { nameof(Model), Model! },
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

    if (fix is { })
    {
      fix = CreateFix();
    }
  }
}
