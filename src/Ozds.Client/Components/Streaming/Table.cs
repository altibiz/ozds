using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
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
  public RenderFragment<T>? Summary { get; set; } = default!;

  [Parameter]
  public RenderFragment<T>? Details { get; set; } = default!;

  [Parameter]
  public RenderFragment<IEnumerable<T>>? Columns { get; set; } = default!;

  [Parameter]
  public int PageCount { get; set; } = QueryConstants.DefaultPageCount;

  [Inject]
  private NavigationManager NavigationManager { get; set; } = default!;

  protected override Task OnParametersSetAsync()
  {
    return dataGrid?.ReloadServerData() ?? Task.CompletedTask;
  }

  private bool Filter(T value)
  {
    if (string.IsNullOrWhiteSpace(searchString))
    {
      return true;
    }

    if (value is IIdentifiable identifiable
      && identifiable.Title.Contains(
        searchString,
        StringComparison.OrdinalIgnoreCase))
    {
      return true;
    }

    return false;
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
    var result = await Fetch(state.Page);
    model = result;
    return new GridData<T>
    {
      Items = result.Items,
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
}
