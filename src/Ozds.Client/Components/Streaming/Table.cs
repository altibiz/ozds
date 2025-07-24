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

  [Parameter]
  public int PageCount { get; set; } = QueryConstants.DefaultPageCount;

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
    PaginatedList<T> result;

    if (PageAsync is not null)
    {
      result = await PageAsync(state.Page);
    }
    else
    {
      result = await Fetch(state.Page);
    }

    model = result;
    return new GridData<T>
    {
      Items = result.Items.Where(FilterItem),
      TotalItems = result.TotalCount
    };
  }

  private async Task<PaginatedList<T>> OnPagingPage(int pageNumber)
  {
    var result = await Fetch(pageNumber);
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

    if (typeof(T).IsAssignableTo(typeof(IModel)))
    {
      var result = await ScopedServices
        .GetRequiredService<ModelQueries>()
        .Read<T>(
          pageNumber,
          CancellationToken,
          PageCount);
      return result;
    }

    return new PaginatedList<T>([], 0);
  }
}
