using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Streaming;

// TODO: memoize Loading parameters to prevent rerendering

public partial class Paging<T> : OzdsOwningComponentBase
  where T : notnull
{
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

  [Inject]
  public IJSRuntime JS { get; set; } = default!;

  private Guid infiniteScrollId = Guid.NewGuid();

  private Loading<PaginatedList<T>>? loading;

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender && Pagination is null && Scroll is Scroll.Infinite)
    {
      var dotNetRef = DotNetObjectReference.Create(this);
      await JS.InvokeVoidAsync("observeElement", infiniteScrollId, dotNetRef);
    }
  }

  [JSInvokable]
  public void OnScrollInView(bool isInView)
  {
    if (isInView && Pagination is null && Scroll is Scroll.Infinite)
    {
      _pageNumber++;
      loading?.ReloadAsync(reset: true);
    }
  }

  private Func<PaginatedList<T>>? OnPage
  {
    get
    {
      return Page is null
        ? null
        : () => Page(_pageNumber);
    }
  }

  private async Task OnSelectedChanged(int pageNumber)
  {
    _pageNumber = pageNumber;
    if (loading is { } nonNullLoading)
    {
      await nonNullLoading.ReloadAsync(reset: true);
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
              pageCount: PageCount)
            .ContinueWith(x => x.Result.Items
              .OfType<T>()
              .ToPaginatedList(x.Result.TotalCount))
          : Page is null && typeof(T).IsAssignableTo(typeof(IReadonly))
            ? () => ScopedServices
              .GetRequiredService<ReadonlyQueries>()
              .ReadDynamic(
                typeof(T),
                _pageNumber,
                CancellationToken,
                pageCount: PageCount)
              .ContinueWith(x => x.Result.Items
                .OfType<T>()
                .ToPaginatedList(x.Result.TotalCount))
            : null;
    }
  }

  private void SetPageNumber(int pageNumber)
  {
    _pageNumber = pageNumber;
    InvokeAsync(StateHasChanged);
  }

  private int _pageNumber = 1;
}
