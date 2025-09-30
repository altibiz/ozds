using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Mutations;
using Ozds.Client.Components.Base;
using Ozds.Client.Components.Dialogs;
using Ozds.Client.State;

namespace Ozds.Client.Components.Streaming;

// TODO: memoize Loading parameters to prevent rerendering

public class Mutating<T> : MappedMutating<T, T>
  where T : notnull
{
}

public partial class MappedMutating<T, TMapped> : OzdsComponentBase
  where T : notnull
{
  // NOTE: when creating start with edit
  private bool creating = true;
  private bool mutating;

  [CascadingParameter]
  public AnalysisState AnalysisState { get; set; } = default!;

  [Parameter]
  public T? Value { get; set; }

  [Parameter]
  public string? Id { get; set; } = default!;

  [Parameter]
  public Func<T, TMapped>? Map { get; set; } = default!;

  [Parameter]
  public RenderFragment? Progress { get; set; }

  [Parameter]
  public RenderFragment? Concretize { get; set; }

  [Parameter]
  public Func<T?>? Load { get; set; }

  [Parameter]
  public Func<Task<T?>>? LoadAsync { get; set; }

  [Parameter]
  public Func<T>? New { get; set; }

  [Parameter]
  public Func<Task<T>>? NewAsync { get; set; }

  [Parameter]
  public string? JoinActivationId { get; set; }

  [Parameter]
  public Type? JoinActivationSide { get; set; }

  [Parameter]
  public RenderFragment<string>? Error { get; set; }

  [Parameter]
  public Action<T>? Create { get; set; }

  [Parameter]
  public Func<T, Task>? CreateAsync { get; set; }

  [Parameter]
  public Action<T>? Update { get; set; }

  [Parameter]
  public Func<T, Task>? UpdateAsync { get; set; }

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
  public Func<T, ActionModel, Exception, RenderFragment>? OnFailureMessage
  {
    get;
    set;
  }

  [Parameter]
  public bool Reload { get; set; }

  [Parameter]
  public RenderFragment<MutatingState<T>>? Details { get; set; } = default!;

  [Parameter]
  public RenderFragment<MutatingState<T>>? Edit { get; set; } = default!;

  [Parameter]
  public RenderFragment<MutatingState<T>>? Footer { get; set; } = default!;

  [Parameter]
  public bool AsReadonly { get; set; } = false;

  [Parameter]
  public bool WithPreview { get; set; } = false;

  [Parameter]
  public bool WithTitle { get; set; } = false;

  [Parameter]
  public bool WithHeading { get; set; } = false;

  [Parameter]
  public bool NotFoundOnCreate { get; set; } = false;

  [Parameter]
  public string? Class { get; set; } = default!;

  [Parameter]
  public string? Style { get; set; } = default!;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  [Inject]
  private IDialogService DialogService { get; set; } = default!;

  private async Task OnCreate(T model)
  {
    object? toCreate = Map is { } map ? map(model) : model;

    var title = toCreate is IIdentifiable identifiable
      ? $" {identifiable.Title}"
      : "";

    try
    {
      if (Create is not null)
      {
        Create(model);
      }
      else if (CreateAsync is not null)
      {
        await CreateAsync(model);
      }
      else if (toCreate is ITrackable trackable)
      {
        var mutations = ScopedServices.GetRequiredService<TrackableMutations>();
        await mutations.Create(trackable, CancellationToken);
      }
      else if (toCreate is IAuditable auditable)
      {
        var mutations = ScopedServices.GetRequiredService<AuditableMutations>();
        await mutations.Create(auditable, CancellationToken);
      }
      else
      {
        throw new InvalidOperationException(
          $"No create strategy found for {typeof(T)}");
      }
    }
    catch (Exception ex)
    {
      var failureMessage = OnFailureMessage is null
        ? Fragment.String($":\n{ex.Message}")
        : OnFailureMessage(model, ActionModel.Create, ex);

      await DialogService.ShowAsync<MutatingResult>(
        Translate("Failure"),
        new DialogParameters
        {
          {
            nameof(MutatingResult.Body),
            Fragment.Combine(
              Fragment.String(
                Translate("Failed creating")
                + " "
                + Translate(typeof(T))
                + title
                + ". "),
              failureMessage)
          },
          {
            nameof(MutatingResult.NavigationBehavior),
            null
          }
        },
        new DialogOptions { CloseOnEscapeKey = true });
      return;
    }

    var successMessage = OnSuccessMessage is null
      ? Fragment.Empty
      : OnSuccessMessage(model, ActionModel.Create);

    await DialogService.ShowAsync<MutatingResult>(
      Translate("Success"),
      new DialogParameters
      {
        {
          nameof(MutatingResult.Body),
          Fragment.Combine(
            Fragment.String(
              Translate("Successfully created")
              + " "
              + Translate(typeof(T))
              + title
              + ". "),
            successMessage)
        },
        {
          nameof(MutatingResult.NavigationBehavior),
          Reload
            ? MutatingResultNavigationBehavior.Reload
            : MutatingResultNavigationBehavior.GoBack
        },
        {
          nameof(MutatingResult.Exit),
          (IMudDialogInstance _) => { AnalysisState.Reset(); }
        }
      },
      new DialogOptions { CloseOnEscapeKey = true });
  }

  private async Task OnUpdate(T model)
  {
    object? toUpdate = Map is { } map ? map(model) : model;

    var title = toUpdate is IIdentifiable identifiable
      ? $" {identifiable.Title}"
      : "";

    try
    {
      if (Update is not null)
      {
        Update(model);
      }
      else if (UpdateAsync is not null)
      {
        await UpdateAsync(model);
      }
      else if (toUpdate is ITrackable trackable)
      {
        var mutations = ScopedServices.GetRequiredService<TrackableMutations>();
        await mutations.Update(trackable, CancellationToken);
      }
      else
      {
        throw new InvalidOperationException(
          $"No update strategy found for {typeof(T)}");
      }
    }
    catch (Exception ex)
    {
      var failureMessage = OnFailureMessage is null
        ? Fragment.String($":\n{ex.Message}")
        : OnFailureMessage(model, ActionModel.Update, ex);

      await DialogService.ShowAsync<MutatingResult>(
        Translate("Failure"),
        new DialogParameters
        {
          {
            nameof(MutatingResult.Body),
            Fragment.Combine(
              Fragment.String(
                Translate("Failed updating")
                + " "
                + Translate(typeof(T))
                + title
                + ". "),
              failureMessage)
          },
          {
            nameof(MutatingResult.NavigationBehavior),
            null
          }
        },
        new DialogOptions { CloseOnEscapeKey = true });
      return;
    }

    var successMessage = OnSuccessMessage is null
      ? Fragment.Empty
      : OnSuccessMessage(model, ActionModel.Update);

    await DialogService.ShowAsync<MutatingResult>(
      Translate("Success"),
      new DialogParameters
      {
        {
          nameof(MutatingResult.Body),
          Fragment.Combine(
            Fragment.String(
              Translate("Successfully updated")
              + " "
              + Translate(typeof(T))
              + title
              + ". "),
            successMessage)
        },
        {
          nameof(MutatingResult.NavigationBehavior),
          Reload
            ? MutatingResultNavigationBehavior.Reload
            : MutatingResultNavigationBehavior.GoBack
        },
        {
          nameof(MutatingResult.Exit),
          (IMudDialogInstance _) => { AnalysisState.Reset(); }
        }
      },
      new DialogOptions { CloseOnEscapeKey = true });
  }

  private async Task OnDelete(T model)
  {
    object? toDelete = Map is { } map ? map(model) : model;

    var title = toDelete is IIdentifiable identifiable
      ? $" {identifiable.Title}"
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
          $"No delete strategy found for {typeof(T)}");
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
                + Translate(typeof(T))
                + title
                + ". "),
              failureMessage)
          },
          {
            nameof(MutatingResult.NavigationBehavior),
            null
          }
        },
        new DialogOptions { CloseOnEscapeKey = true });
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
              + Translate(typeof(T))
              + title
              + ". "),
            successMessage)
        },
        {
          nameof(MutatingResult.NavigationBehavior),
          Reload
            ? MutatingResultNavigationBehavior.Reload
            : MutatingResultNavigationBehavior.GoBack
        },
        {
          nameof(MutatingResult.Exit),
          (IMudDialogInstance _) => { AnalysisState.Reset(); }
        }
      },
      new DialogOptions { CloseOnEscapeKey = true });
  }

  private async Task OnRestore(T model)
  {
    object? toRestore = Map is { } map ? map(model) : model;

    var title = toRestore is IIdentifiable identifiable
      ? $" {identifiable.Title}"
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
          $"No restore strategy found for {typeof(T)}");
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
                + Translate(typeof(T))
                + title
                + ". "),
              failureMessage)
          },
          {
            nameof(MutatingResult.NavigationBehavior),
            null
          }
        },
        new DialogOptions { CloseOnEscapeKey = true });
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
              + Translate(typeof(T))
              + title
              + ". "),
            successMessage)
        },
        {
          nameof(MutatingResult.NavigationBehavior),
          Reload
            ? MutatingResultNavigationBehavior.Reload
            : MutatingResultNavigationBehavior.GoBack
        },
        {
          nameof(MutatingResult.Exit),
          (IMudDialogInstance _) => { AnalysisState.Reset(); }
        }
      },
      new DialogOptions { CloseOnEscapeKey = true });
  }

  private async Task OnForget(T model)
  {
    object? toForget = Map is { } map ? map(model) : model;

    var title = toForget is IIdentifiable identifiable
      ? $" {identifiable.Title}"
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
          $"No forget strategy found for {typeof(T)}");
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
                + Translate(typeof(T))
                + title
                + ". "),
              failureMessage)
          },
          {
            nameof(MutatingResult.NavigationBehavior),
            null
          }
        },
        new DialogOptions { CloseOnEscapeKey = true });
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
              + Translate(typeof(T))
              + title
              + ". "),
            successMessage)
        },
        {
          nameof(MutatingResult.NavigationBehavior),
          Reload
            ? MutatingResultNavigationBehavior.Reload
            : MutatingResultNavigationBehavior.GoBack
        },
        {
          nameof(MutatingResult.Exit),
          (IMudDialogInstance _) => { AnalysisState.Reset(); }
        }
      },
      new DialogOptions { CloseOnEscapeKey = true });
  }
}
