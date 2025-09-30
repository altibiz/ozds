using System.Collections;
using MudBlazor;
using Ozds.Business.Queries.Abstractions;

namespace Ozds.Client.State;

public record TitleState
{
  public static implicit operator TitleState(
    LoadingState loadingState)
  {
    return new LoadingTitleState(loadingState);
  }

  public static implicit operator TitleState(
    MutatingState mutatingState)
  {
    return new MutatingTitleState(mutatingState);
  }

  public static implicit operator TitleState(
    PaginatedList paginatedList)
  {
    return new PaginatedListTitleState(paginatedList);
  }

  public static implicit operator TitleState(
    MudGridData mudGridData
  )
  {
    return new MudGridDataTitleState(mudGridData);
  }
}

public record LoadingTitleState(LoadingState LoadingState) : TitleState;

public record MutatingTitleState(MutatingState MutatingState) : TitleState;

public record PaginatedListTitleState(PaginatedList PaginatedList) : TitleState;

public record MudGridDataTitleState(MudGridData MudGridData) : TitleState;

public record MudGridData(Type Type, IEnumerable ObjectItems, int TotalItems);

public record MudGridData<T>(IEnumerable<T> Items, int TotalItems)
  : MudGridData(typeof(T), Items, TotalItems)
{
  public static implicit operator MudGridData<T>(
    GridData<T> gridData)
  {
    return new MudGridData<T>(
      gridData.Items,
      gridData.TotalItems
    );
  }
}
