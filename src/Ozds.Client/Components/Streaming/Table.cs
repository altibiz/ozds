using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Streaming;

public partial class Table<T> : OzdsComponentBase
{
  private MudDataGrid<T>? dataGrid;

  private PaginatedList<T> model = new([], 0);

  private string? searchString;

  [Parameter]
  public bool Deleted { get; set; } = false;

  [Parameter]
  public IEnumerable<T>? Model { get; set; }

  [Parameter]
  public Func<int, PaginatedList<T>>? Page { get; set; }

  [Parameter]
  public Func<int, Task<PaginatedList<T>>>? PageAsync { get; set; }

  [Parameter]
  public Func<T, bool>? Filter { get; set; }

  [Parameter]
  public RenderFragment<T>? Summary { get; set; } = default!;

  [Parameter]
  public RenderFragment<T>? Details { get; set; } = default!;

  [Parameter]
  public RenderFragment<IEnumerable<T>>? Columns { get; set; } = default!;

  public int PageCount => string.IsNullOrWhiteSpace(searchString)
       ? QueryConstants.DefaultPageCount
       : QueryConstants.DefaultLargePageCount;

  [Parameter]
  public bool DynamicTitle { get; set; } = false;

  [Parameter]
  public string Class { get; set; } = string.Empty;

  [Parameter]
  public string Style { get; set; } = string.Empty;

  [Inject]
  private NavigationManager NavigationManager { get; set; } = default!;

  protected override Task OnParametersSetAsync()
  {
    return dataGrid?.ReloadServerData() ?? Task.CompletedTask;
  }

  private bool FilterItem(T value)
  {
    if (Filter is not null)
    {
      return Filter(value);
    }

    if (value is null)
    {
      return false;
    }

    if (string.IsNullOrEmpty(searchString))
    {
      return true;
    }

    if (value is IIdentifiable rootIdent && ContainsSearch(rootIdent.Title))
    {
      return true;
    }

    if (value is IIdentifiable)
    {
      return false;
    }

    var props = value.GetType()
      .GetProperties(BindingFlags.Public | BindingFlags.Instance)
      .Where(p => p.CanRead);

    foreach (var prop in props)
    {
      var propVal = prop.GetValue(value);
      if (propVal == null)
      {
        continue;
      }

      if (propVal is IIdentifiable childIdent
        && ContainsSearch(childIdent.Title))
      {
        return true;
      }
    }

    return false;
  }

  private bool ContainsSearch(string? text)
  {
    if (string.IsNullOrWhiteSpace(searchString))
    {
      return true;
    }

    return !string.IsNullOrEmpty(text)
      && text.Contains(searchString, StringComparison.OrdinalIgnoreCase);
  }

  private Task OnPagingSearch(string newSearchString)
  {
    searchString = newSearchString;
    return Task.CompletedTask;
  }

  private Task OnDataGridSearch(string newSearchString)
  {
    searchString = newSearchString;
    return dataGrid?.ReloadServerData() ?? Task.CompletedTask;
  }

  private async Task<GridData<T>> OnDataGridServerData(GridState<T> state)
  {
    var smartPageNumber = !string.IsNullOrEmpty(searchString) ? 0 : state.Page;
    PaginatedList<T> result;

    if (PageAsync is not null)
    {
      result = await PageAsync(smartPageNumber);
    }
    else
    {
      result = await Fetch(smartPageNumber);
    }
    model = result;

    var sortDef = state.SortDefinitions.FirstOrDefault();
    if (sortDef is not null && dataGrid is not null)
    {
      var col = dataGrid.RenderedColumns
          .First(c => c.PropertyName == sortDef.SortBy);

      var colType = col.GetType();
      var propPropInfo = colType.GetProperty(
          "Property",
          BindingFlags.Instance | BindingFlags.Public
      );
      if (propPropInfo == null)
      {
        throw new InvalidOperationException(
              $"Column type {colType.Name} has no public ‘Property’ parameter"
          );
      }

      var lambda = (LambdaExpression?)propPropInfo.GetValue(col);
      if (lambda == null)
      {
        throw new InvalidOperationException(
              "The column’s Property parameter was null—are you sure this is a PropertyColumn?"
          );
      }

      var pi = GetPropertyInfoFromExpression(lambda);
    }

    return new GridData<T>
    {
      Items = result.Items.Where(FilterItem),
      TotalItems = result.TotalCount
    };
  }

  private async Task<PaginatedList<T>> OnPagingPage(int pageNumber)
  {
    var smartPageNumber = !string.IsNullOrEmpty(searchString) ? 0 : pageNumber;
    var result = await Fetch(smartPageNumber);
    model = result;
    return result;
  }

  private async Task<PaginatedList<T>> Fetch(int pageNumber)
  {
    if (Model is { } nonNullModel)
    {
      var result = new PaginatedList<T>(
        nonNullModel.Skip(pageNumber * PageCount).Take(PageCount).ToList(),
        nonNullModel.Count()
      );
      return result;
    }

    if (Page is { } page)
    {
      var result = page(pageNumber);
      return result;
    }

    if (PageAsync is { } pageAsync)
    {
      var result = await pageAsync(pageNumber);
      return result;
    }

    if (typeof(T).IsAssignableTo(typeof(IAuditable)))
    {
      var result = await ScopedServices
        .GetRequiredService<AuditableQueries>()
        .Read<T>(
          pageNumber,
          CancellationToken,
          PageCount,
          Deleted);
      return result;
    }

    if (typeof(T).IsAssignableTo(typeof(IReadonly)))
    {
      var result = await ScopedServices
        .GetRequiredService<ReadonlyQueries>()
        .Read<T>(
          pageNumber,
          CancellationToken,
          PageCount);
      return result;
    }

    return new PaginatedList<T>([], 0);
  }

  private static PropertyInfo GetPropertyInfoFromExpression(LambdaExpression lambda)
  {
    Expression body = lambda.Body;
    if (body is ConditionalExpression cond)
    {
      body = cond.IfFalse;
    }

    while (body is UnaryExpression u &&
           (u.NodeType == ExpressionType.Convert ||
            u.NodeType == ExpressionType.ConvertChecked))
    {
      body = u.Operand;
    }

    if (body is not MemberExpression member)
    {
      throw new InvalidOperationException(
          $"Expression is not a member access: {body.GetType().Name}");
    }

    if (member.Member is not PropertyInfo pi)
    {
      throw new InvalidOperationException(
          $"Member '{member.Member.Name}' is not a property");
    }

    return pi;
  }
}
