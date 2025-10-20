using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Streaming;

public class Paging<T> : MappedPaging<T, T>
  where T : notnull
{
}

public partial class MappedPaging<T, TMapped> : OzdsComponentBase
  where T : notnull
{
  private readonly Guid infiniteScrollId = Guid.NewGuid();

  private MappedLoading<PaginatedList<T>, PaginatedList<TMapped>>? loading;

  private int pageNumber = 0;

  [Parameter]
  public IEnumerable<T>? Value { get; set; } = default!;

  [Parameter]
  public Func<T, TMapped>? Map { get; set; } = default!;

  [Parameter]
  public Func<int, int, PaginatedList<T>>? Page { get; set; }

  [Parameter]
  public Func<int, int, Task<PaginatedList<T>>>? PageAsync { get; set; }

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

  [Parameter]
  public string Class { get; set; } = string.Empty;

  [Parameter]
  public string Style { get; set; } = string.Empty;

  [Inject]
  public IJSRuntime JS { get; set; } = default!;

  private PaginatedList<T>? LoadingValue
  {
    get
    {
      return Value is { } value
        ? PageFetched(value)
        : null;
    }
  }

  private Func<PaginatedList<T>, PaginatedList<TMapped>>? LoadingMap
  {
    get
    {
      return Map is { } map
        ? x => x.Items.Select(map).ToPaginatedList(x.TotalCount)
        : null;
    }
  }

  private Func<PaginatedList<T>>? OnPage
  {
    get
    {
      if (Page is not null)
      {
        return () => Page(pageNumber, PageCount);
      }

      return null;
    }
  }

  // NOTE: check for all of these because of sorting/filtering rules
  private Func<Task<PaginatedList<T>>>? OnPageAsync
  {
    get
    {
      if (PageAsync is not null)
      {
        return () => PageAsync(pageNumber, PageCount);
      }

      if (typeof(T).IsAssignableTo(typeof(ITrackable)))
      {
        return () => ScopedServices
          .GetRequiredService<TrackableQueries>()
          .Read(
            typeof(T),
            pageNumber,
            CancellationToken,
            PageCount,
            Deleted)
          .ContinueWith(
            x => x.IsCanceled
              ? new PaginatedList<T>(new List<T>(), 0)
              : x.Result.Items
                .OfType<T>()
                .ToPaginatedList(x.Result.TotalCount));
      }

      if (typeof(T).IsAssignableTo(typeof(IAuditable)))
      {
        return () => ScopedServices
          .GetRequiredService<AuditableQueries>()
          .Read(
            typeof(T),
            pageNumber,
            CancellationToken,
            PageCount)
          .ContinueWith(
            x => x.IsCanceled
              ? new PaginatedList<T>(new List<T>(), 0)
              : x.Result.Items
                .OfType<T>()
                .ToPaginatedList(x.Result.TotalCount));
      }

      if (typeof(T).IsAssignableTo(typeof(IIdentifiable)))
      {
        return () => ScopedServices
          .GetRequiredService<IdentifiableQueries>()
          .Read(
            typeof(T),
            pageNumber,
            CancellationToken,
            PageCount)
          .ContinueWith(
            x => x.IsCanceled
              ? new PaginatedList<T>(new List<T>(), 0)
              : x.Result.Items
                .OfType<T>()
                .ToPaginatedList(x.Result.TotalCount));
      }

      if (typeof(T).IsAssignableTo(typeof(IModel)))
      {
        return () => ScopedServices
          .GetRequiredService<ModelQueries>()
          .Read(
            typeof(T),
            pageNumber,
            CancellationToken,
            PageCount)
          .ContinueWith(
            x => x.IsCanceled
              ? new PaginatedList<T>(new List<T>(), 0)
              : x.Result.Items
                .OfType<T>()
                .ToPaginatedList(x.Result.TotalCount));
      }

      return null;
    }
  }

  public async Task Fetch()
  {
    await FetchLoading();
  }

  [JSInvokable]
  public async Task OnScrollInView(
    string elementId,
    bool isInView
  )
  {
    if (elementId != infiniteScrollId.ToString())
    {
      return;
    }

    if (isInView && Pagination is null && Scroll is Scroll.Infinite)
    {
      pageNumber++;
      await FetchLoading();
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

  private async Task SetPageNumber(int pageNumber)
  {
    this.pageNumber = pageNumber - 1;
    await FetchLoading();
  }

  private async Task FetchLoading()
  {
    if (loading is null)
    {
      return;
    }

    await loading.Fetch();
  }

  private PaginatedList<T>? PageFetched(
    IEnumerable<T> value
  )
  {
    return value
      .Skip(pageNumber * PageCount)
      .Take(PageCount)
      .ToPaginatedList(value.Count());
  }
}
