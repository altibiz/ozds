using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Streaming;

// TODO: memoize Loading parameters to prevent rerendering

public partial class Paging<T> : OzdsComponentBase
  where T : notnull
{
  private int _pageNumber = 1;

  private Guid infiniteScrollId = Guid.NewGuid();

  private Loading<PaginatedList<T>>? loading;

  [Parameter]
  public Func<int, PaginatedList<T>>? Page { get; set; }

  [Parameter]
  public Func<int, Task<PaginatedList<T>>>? PageAsync { get; set; }

  [Parameter]
  public RenderFragment? Progress { get; set; }

  [Parameter]
  public RenderFragment<string>? Error { get; set; }

  [Parameter]
  public RenderFragment<T>? Summary { get; set; }

  [Parameter]
  public RenderFragment<PaginatedList<T>>? Found { get; set; }

  [Parameter]
  public RenderFragment? Empty { get; set; }

  [Parameter]
  public int PageCount { get; set; } = QueryConstants.DefaultPageCount;

  [Parameter]
  public RenderFragment<PaginationOptions>? Pagination { get; set; }

  [Parameter]
  public Scroll Scroll { get; set; } = Scroll.Paged;

  [Parameter]
  public bool Deleted { get; set; } = false;

  [Inject]
  public IJSRuntime JS { get; set; } = default!;

  private Func<PaginatedList<T>>? OnPage
  {
    get
    {
      return Page is null
        ? null
        : () => Page(_pageNumber);
    }
  }

  private Func<Task<PaginatedList<T>>>? OnPageAsync
  {
    get
    {
      return PageAsync is not null
        ? () => PageAsync(_pageNumber)
        : Page is null && typeof(T).IsAssignableTo(typeof(IAuditable))
          ? () => ScopedServices
            .GetRequiredService<AuditableQueries>()
            .ReadDynamic(
              typeof(T),
              _pageNumber,
              CancellationToken,
              PageCount,
              Deleted)
            .ContinueWith(
              x => x.IsCanceled
                ? new PaginatedList<T>(new List<T>(), 0)
                : x.Result.Items
                  .OfType<T>()
                  .ToPaginatedList(x.Result.TotalCount))
          : Page is null && typeof(T).IsAssignableTo(typeof(IReadonly))
            ? () => ScopedServices
              .GetRequiredService<ReadonlyQueries>()
              .ReadDynamic(
                typeof(T),
                _pageNumber,
                CancellationToken,
                PageCount)
              .ContinueWith(
                x => x.IsCanceled
                  ? new PaginatedList<T>(new List<T>(), 0)
                  : x.Result.Items
                    .OfType<T>()
                    .ToPaginatedList(x.Result.TotalCount))
            : null;
    }
  }

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender && Pagination is null && Scroll is Scroll.Infinite)
    {
      var dotNetRef = DotNetObjectReference.Create(this);
      await JS.InvokeVoidAsync("observeElement", infiniteScrollId, dotNetRef);
    }
  }

  protected override void OnParametersSet()
  {
    if (loading is null)
    {
      return;
    }

    loading.Reload(true);
  }

  protected override async Task OnParametersSetAsync()
  {
    if (loading is null)
    {
      return;
    }

    await loading.ReloadAsync(true);
  }

  [JSInvokable]
  public void OnScrollInView(bool isInView)
  {
    if (isInView && Pagination is null && Scroll is Scroll.Infinite)
    {
      _pageNumber++;
      loading?.ReloadAsync(true);
    }
  }

  private async Task OnSelectedChanged(int pageNumber)
  {
    _pageNumber = pageNumber;
    if (loading is { } nonNullLoading)
    {
      await nonNullLoading.ReloadAsync(true);
    }
  }

  private void SetPageNumber(int pageNumber)
  {
    _pageNumber = pageNumber;
    InvokeAsync(StateHasChanged);
  }
}
