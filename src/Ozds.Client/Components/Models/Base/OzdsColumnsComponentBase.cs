using System.Collections.Immutable;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Query;

namespace Ozds.Client.Components.Models.Base;

public abstract partial class OzdsColumnsComponentBase<TPrefix, TModel>
  : OzdsListModelComponentBase<TPrefix, TModel>
{
  private readonly Dictionary<Expression, Func<object, object?>> searchMappers =
    new Dictionary<Expression, Func<object, object?>>(
      ExpressionEqualityComparer.Instance
    );

  [Parameter]
  public EventCallback<
    IEnumerable<Func<object, object?>>
  > SearchMappersChanged { get; set; }

  protected virtual int NestingLevel
  {
    get { return 1; }
  }

  public override ModelComponentKind ComponentKind
  {
    get { return ModelComponentKind.Columns; }
  }

  // NOTE: this adds slight overhead but it returns thread safe snapshot of the mappers
  //       instead of directly returning intern dict values
  public IEnumerable<Func<object, object?>> GetSearchMappers()
  {
    return searchMappers.Values.ToImmutableList();
  }

  protected void RegisterSearchMapper<T>(
    Expression expr,
    Func<TModel, T> mapperFunc
  )
  {
    searchMappers.TryAdd(expr, model => mapperFunc((TModel)model));
  }

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    await base.OnAfterRenderAsync(firstRender);

    if (firstRender && SearchMappersChanged.HasDelegate)
    {
      await SearchMappersChanged.InvokeAsync(GetSearchMappers());
    }
  }
}
