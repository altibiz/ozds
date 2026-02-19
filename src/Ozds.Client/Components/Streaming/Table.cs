using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Mutations;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Base;
using Ozds.Client.Components.Dialogs;
using Ozds.Client.Components.Models;
using Ozds.Client.State;

namespace Ozds.Client.Components.Streaming;

public class Table<T> : MappedTable<T, T>
  where T : notnull { }

public partial class MappedTable<T, TMapped> : OzdsComponentBase
  where T : notnull
{
  private bool checkedDeleted;

  private MudDataGrid<T>? dataGrid;

  private PaginatedList<T> fetchedModel = new([], 0);

  private MappedPaging<T, TMapped>? paging;

  private string? searchString;

  [CascadingParameter]
  public RepresentativeState RepresentativeState { get; set; } = default!;

  [CascadingParameter]
  public AnalysisState AnalysisState { get; set; } = default!;

  [Inject]
  private ModelComponentProvider ModelComponentProvider { get; set; } =
    default!;

  [Inject]
  private IDialogService DialogService { get; set; } = default!;

  [Parameter]
  public bool Deleted { get; set; }

  [Parameter]
  public IEnumerable<T>? Value { get; set; }

  [Parameter]
  public Func<string, int, int, PaginatedList<T>>? Page { get; set; }

  [Parameter]
  public Func<string, int, int, Task<PaginatedList<T>>>? PageAsync { get; set; }

  [Parameter]
  public int PageCount { get; set; } = QueryConstants.DefaultPageCount;

  [Parameter]
  public RenderFragment<T>? Summary { get; set; }

  [Parameter]
  public RenderFragment<T>? Details { get; set; }

  [Parameter]
  public RenderFragment<IEnumerable<T>>? Columns { get; set; }

  [Parameter]
  public RenderFragment<IEnumerable<T>>? Tools { get; set; }

  [Parameter]
  public RenderFragment<T>? Actions { get; set; }

  [Parameter]
  public RenderFragment? Empty { get; set; }

  [Parameter]
  public bool WithDeleted { get; set; }

  [Parameter]
  public bool WithMutations { get; set; }

  [Parameter]
  public Func<T, TMapped>? Map { get; set; }

  [Parameter]
  public bool WithCreate { get; set; }

  [Parameter]
  public bool WithHeading { get; set; }

  [Parameter]
  public bool WithTitle { get; set; }

  [Parameter]
  public string Class { get; set; } = string.Empty;

  [Parameter]
  public string Style { get; set; } = string.Empty;

  [Parameter]
  public Action<T>? Delete { get; set; }

  [Parameter]
  public Func<T, Task>? DeleteAsync { get; set; }

  [Parameter]
  public Action<T>? Restore { get; set; }

  [Parameter]
  public Func<T, Task>? RestoreAsync { get; set; }

  [Parameter]
  public Action<T>? Forget { get; set; }

  [Parameter]
  public Func<T, Task>? ForgetAsync { get; set; }

  [Parameter]
  public Func<T, ActionModel, RenderFragment>? OnSuccessMessage { get; set; }

  [Parameter]
  public Func<
    T,
    ActionModel,
    Exception,
    RenderFragment
  >? OnFailureMessage { get; set; }

  private IEnumerable<T>? FilteredValue
  {
    get { return Value is { } value ? value.Where(FilterFetched) : null; }
  }

  public async Task Fetch()
  {
    await FetchPaging();
    await FetchDataGrid();
  }

  protected override void OnParametersSet()
  {
    checkedDeleted = Deleted;
  }

  private async Task OnDeletedChanged()
  {
    checkedDeleted = !checkedDeleted;
    await FetchPaging();
    await FetchDataGrid();
  }

  private async Task OnPagingSearch(string newSearchString)
  {
    searchString = newSearchString;
    await FetchPaging();
  }

  private async Task OnDataGridSearch(string newSearchString)
  {
    searchString = newSearchString;
    await FetchDataGrid();
  }

  private async Task<GridData<T>> OnDataGridServerData(GridState<T> state)
  {
    var result = await Reload(state.Page, state.PageSize);
    fetchedModel = result;
    return new GridData<T>
    {
      Items = result.Items,
      TotalItems = result.TotalCount,
    };
  }

  private async Task<PaginatedList<T>> OnPagingPage(
    int pageNumber,
    int pageCount
  )
  {
    var result = await Reload(pageNumber, pageCount);
    fetchedModel = result;
    return result;
  }

  private async Task OnDelete(T model)
  {
    object? toDelete = Map is null ? model : Map(model);

    var toDeleteTitle = toDelete is IIdentifiable toDeleteIdentifiable
      ? $" {toDeleteIdentifiable.Title}"
      : "";

    try
    {
      if (Delete is not null)
      {
        Delete(model);
      }
      else if (DeleteAsync is not null)
      {
        await DeleteAsync(model);
      }
      else if (toDelete is ITrackable trackable)
      {
        var mutations = ScopedServices.GetRequiredService<TrackableMutations>();
        await mutations.Delete(trackable, CancellationToken);
      }
      else if (toDelete is IAuditable auditable)
      {
        var mutations = ScopedServices.GetRequiredService<AuditableMutations>();
        await mutations.Delete(auditable, CancellationToken);
      }
      else
      {
        throw new InvalidOperationException(
          $"No delete strategy found for {typeof(T)}"
        );
      }
    }
    catch (Exception ex)
    {
      var failureMessage = OnFailureMessage is null
        ? Fragment.String($":\n{ex.Message}")
        : OnFailureMessage(model, ActionModel.Delete, ex);

      await DialogService.ShowAsync<MutatingResult>(
        Translate("Failure"),
        new DialogParameters
        {
          {
            nameof(MutatingResult.Body),
            Fragment.Combine(
              Fragment.String(
                Translate("Failed deleting")
                  + " "
                  + Translate(typeof(TMapped))
                  + toDeleteTitle
                  + ". "
              ),
              failureMessage
            )
          },
        },
        new DialogOptions { CloseOnEscapeKey = true }
      );
      return;
    }

    var successMessage = OnSuccessMessage is null
      ? Fragment.Empty
      : OnSuccessMessage(model, ActionModel.Delete);

    await DialogService.ShowAsync<MutatingResult>(
      Translate("Success"),
      new DialogParameters
      {
        {
          nameof(MutatingResult.Body),
          Fragment.Combine(
            Fragment.String(
              Translate("Successfully deleted")
                + " "
                + Translate(typeof(TMapped))
                + toDeleteTitle
                + ". "
            ),
            successMessage
          )
        },
        {
          nameof(MutatingResult.Exit),
          (IMudDialogInstance _) =>
          {
            AnalysisState.Reset();
          }
        },
        { nameof(MutatingResult.NavigationBehavior), null },
      },
      new DialogOptions { CloseOnEscapeKey = true }
    );
  }

  private async Task OnRestore(T model)
  {
    object? toRestore = Map is null ? model : Map(model);

    var toRestoreTitle = toRestore is IIdentifiable toRestoreIdentifiable
      ? $" {toRestoreIdentifiable.Title}"
      : "";

    try
    {
      if (Restore is not null)
      {
        Restore(model);
      }
      else if (RestoreAsync is not null)
      {
        await RestoreAsync(model);
      }
      else if (toRestore is ITrackable trackable)
      {
        var mutations = ScopedServices.GetRequiredService<TrackableMutations>();
        await mutations.Restore(trackable, CancellationToken);
      }
      else
      {
        throw new InvalidOperationException(
          $"No restore strategy found for {typeof(T)}"
        );
      }
    }
    catch (Exception ex)
    {
      var failureMessage = OnFailureMessage is null
        ? Fragment.String($":\n{ex.Message}")
        : OnFailureMessage(model, ActionModel.Restore, ex);

      await DialogService.ShowAsync<MutatingResult>(
        Translate("Failure"),
        new DialogParameters
        {
          {
            nameof(MutatingResult.Body),
            Fragment.Combine(
              Fragment.String(
                Translate("Failed restoring")
                  + " "
                  + Translate(typeof(TMapped))
                  + toRestoreTitle
                  + ". "
              ),
              failureMessage
            )
          },
        },
        new DialogOptions { CloseOnEscapeKey = true }
      );
      return;
    }

    var successMessage = OnSuccessMessage is null
      ? Fragment.Empty
      : OnSuccessMessage(model, ActionModel.Restore);

    await DialogService.ShowAsync<MutatingResult>(
      Translate("Success"),
      new DialogParameters
      {
        {
          nameof(MutatingResult.Body),
          Fragment.Combine(
            Fragment.String(
              Translate("Successfully restored")
                + " "
                + Translate(typeof(TMapped))
                + toRestoreTitle
                + ". "
            ),
            successMessage
          )
        },
        {
          nameof(MutatingResult.Exit),
          (IMudDialogInstance _) =>
          {
            AnalysisState.Reset();
          }
        },
        { nameof(MutatingResult.NavigationBehavior), null },
      },
      new DialogOptions { CloseOnEscapeKey = true }
    );
  }

  private async Task OnForget(T model)
  {
    object? toForget = Map is null ? model : Map(model);

    var toForgetTitle = toForget is IIdentifiable toForgetIdentifiable
      ? $" {toForgetIdentifiable.Title}"
      : "";

    try
    {
      if (Forget is not null)
      {
        Forget(model);
      }
      else if (ForgetAsync is not null)
      {
        await ForgetAsync(model);
      }
      else if (toForget is ITrackable trackable)
      {
        var mutations = ScopedServices.GetRequiredService<TrackableMutations>();
        await mutations.Forget(trackable, CancellationToken);
      }
      else
      {
        throw new InvalidOperationException(
          $"No forget strategy found for {typeof(T)}"
        );
      }
    }
    catch (Exception ex)
    {
      var failureMessage = OnFailureMessage is null
        ? Fragment.String($":\n{ex.Message}")
        : OnFailureMessage(model, ActionModel.Forget, ex);

      await DialogService.ShowAsync<MutatingResult>(
        Translate("Failure"),
        new DialogParameters
        {
          {
            nameof(MutatingResult.Body),
            Fragment.Combine(
              Fragment.String(
                Translate("Failed forgetting")
                  + " "
                  + Translate(typeof(TMapped))
                  + toForgetTitle
                  + ". "
              ),
              failureMessage
            )
          },
        },
        new DialogOptions { CloseOnEscapeKey = true }
      );
      return;
    }

    var successMessage = OnSuccessMessage is null
      ? Fragment.Empty
      : OnSuccessMessage(model, ActionModel.Forget);

    await DialogService.ShowAsync<MutatingResult>(
      Translate("Success"),
      new DialogParameters
      {
        {
          nameof(MutatingResult.Body),
          Fragment.Combine(
            Fragment.String(
              Translate("Successfully forgotten")
                + " "
                + Translate(typeof(TMapped))
                + toForgetTitle
                + ". "
            ),
            successMessage
          )
        },
        {
          nameof(MutatingResult.Exit),
          (IMudDialogInstance _) =>
          {
            AnalysisState.Reset();
          }
        },
        { nameof(MutatingResult.NavigationBehavior), null },
      },
      new DialogOptions { CloseOnEscapeKey = true }
    );
  }

  private async Task FetchPaging()
  {
    if (paging is null)
    {
      return;
    }

    await paging.Fetch();
  }

  private async Task FetchDataGrid()
  {
    if (dataGrid is null)
    {
      return;
    }

    await dataGrid.ReloadServerData();
  }

  private async Task<PaginatedList<T>> Reload(int pageNumber, int pageCount)
  {
    if (Value is { } value)
    {
      return PageFetched(value, pageNumber, pageCount);
    }

    if (Page is { } page)
    {
      var result = page(searchString ?? string.Empty, pageNumber, pageCount);
      return result;
    }

    if (PageAsync is { } pageAsync)
    {
      var result = await pageAsync(
        searchString ?? string.Empty,
        pageNumber,
        pageCount
      );
      return result;
    }

    if (typeof(T).IsAssignableTo(typeof(ITrackableIdentifiable)))
    {
      var queries = ScopedServices.GetRequiredService<TrackableQueries>();

      if (string.IsNullOrWhiteSpace(searchString))
      {
        var result = await queries.Read(
          typeof(T),
          pageNumber,
          CancellationToken,
          pageCount,
          checkedDeleted
        );
        return result.Items.OfType<T>().ToPaginatedList(result.TotalCount);
      }
      else
      {
        var result = await queries.ReadByTitle(
          typeof(T),
          searchString,
          pageNumber,
          CancellationToken,
          pageCount,
          checkedDeleted
        );
        return result.Items.OfType<T>().ToPaginatedList(result.TotalCount);
      }
    }

    if (typeof(T).IsAssignableTo(typeof(IAuditableIdentifiable)))
    {
      var queries = ScopedServices.GetRequiredService<AuditableQueries>();

      if (string.IsNullOrWhiteSpace(searchString))
      {
        var result = await queries.Read(
          typeof(T),
          pageNumber,
          CancellationToken,
          pageCount
        );
        return result.Items.OfType<T>().ToPaginatedList(result.TotalCount);
      }
      else
      {
        var result = await queries.ReadByTitle(
          typeof(T),
          searchString,
          pageNumber,
          CancellationToken,
          pageCount
        );
        return result.Items.OfType<T>().ToPaginatedList(result.TotalCount);
      }
    }

    if (typeof(T).IsAssignableTo(typeof(IIdentifiable)))
    {
      var queries = ScopedServices.GetRequiredService<IdentifiableQueries>();

      if (string.IsNullOrWhiteSpace(searchString))
      {
        var result = await queries.Read(
          typeof(T),
          pageNumber,
          CancellationToken,
          pageCount
        );
        return result.Items.OfType<T>().ToPaginatedList(result.TotalCount);
      }
      else
      {
        var result = await queries.ReadByTitle(
          typeof(T),
          searchString,
          pageNumber,
          CancellationToken,
          pageCount
        );
        return result.Items.OfType<T>().ToPaginatedList(result.TotalCount);
      }
    }

    if (typeof(T).IsAssignableTo(typeof(IModel)))
    {
      var result = await ScopedServices
        .GetRequiredService<ModelQueries>()
        .Read(typeof(T), pageNumber, CancellationToken, pageCount);
      return result.Items.OfType<T>().ToPaginatedList(result.TotalCount);
    }

    return new PaginatedList<T>([], 0);
  }

  private PaginatedList<T> PageFetched(
    IEnumerable<T> value,
    int pageNumber,
    int pageCount
  )
  {
    return value
      .Where(FilterFetched)
      .Skip(pageNumber * pageCount)
      .Take(pageCount)
      .ToPaginatedList(value.Count());
  }

  private bool FilterFetched(T model)
  {
    if (string.IsNullOrWhiteSpace(searchString))
    {
      return true;
    }

    if (model is null)
    {
      return false;
    }

    object? toFilter = Map is null ? model : Map(model);

    if (toFilter is null)
    {
      return false;
    }

    if (model is ITrackable trackable && trackable.IsDeleted != checkedDeleted)
    {
      return false;
    }

    if (
      model is IIdentifiable { Title: { } title }
      && title.Contains(searchString)
    )
    {
      return true;
    }

    return false;
  }
}
