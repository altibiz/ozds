using System.Text.Json;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations;
using Ozds.Client.Components.Base;
using Ozds.Client.Components.Dialogs;
using Ozds.Client.State;

namespace Ozds.Client.Components.Streaming;

// TODO: memoize Loading parameters to prevent rerendering

public partial class Mutating<T> : OzdsComponentBase
  where T : notnull
{
  private bool _mutating;

  [Parameter]
  public T? Value { get; set; }

  [Parameter]
  public string? Id { get; set; } = default!;

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
  public RenderFragment<MutatingState<T>>? Label { get; set; } = default!;

  [Parameter]
  public RenderFragment<MutatingState<T>> Details { get; set; } = default!;

  [Parameter]
  public RenderFragment<MutatingState<T>> Edit { get; set; } = default!;

  [Parameter]
  public bool AsReadonly { get; set; } = false;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  [Inject]
  private IDialogService DialogService { get; set; } = default!;

  private async Task OnCreate(T model)
  {
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
      else if (model is IAuditable auditable)
      {
        var mutations = ScopedServices.GetRequiredService<AuditableMutations>();
        await mutations.Create(auditable, CancellationToken);
      }
      else
      {
        throw new InvalidOperationException(
          $"No create strategy found for {typeof(T).Name}");
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine(JsonSerializer.Serialize(model));
      await DialogService.ShowAsync<MutatingResult>(
        Translate("Failure"),
        new DialogParameters
        {
          {
            nameof(MutatingResult.Body),
            $"{Translate("Failed creating")}"
            + $" {Translate(typeof(T).Name)} - {ex.Message}"
          }
        },
        new DialogOptions { CloseOnEscapeKey = true });
      return;
    }

    await DialogService.ShowAsync<MutatingResult>(
      Translate("Success"),
      new DialogParameters
      {
        {
          nameof(MutatingResult.Body),
          $"{Translate("Successfully created")} {Translate(typeof(T).Name)}"
        }
      },
      new DialogOptions { CloseOnEscapeKey = true });
  }

  private async Task OnUpdate(T model)
  {
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
      else if (model is IAuditable auditable)
      {
        var mutations = ScopedServices.GetRequiredService<AuditableMutations>();
        await mutations.Update(auditable, CancellationToken);
      }
      else
      {
        throw new InvalidOperationException(
          $"No update strategy found for {typeof(T).Name}");
      }
    }
    catch (Exception ex)
    {
      await DialogService.ShowAsync<MutatingResult>(
        Translate("Failure"),
        new DialogParameters
        {
          {
            nameof(MutatingResult.Body),
            $"{Translate("Failed updating")}"
            + $" {Translate(typeof(T).Name)} - {ex.Message}"
          }
        },
        new DialogOptions { CloseOnEscapeKey = true });
      return;
    }

    await DialogService.ShowAsync<MutatingResult>(
      Translate("Success"),
      new DialogParameters
      {
        {
          nameof(MutatingResult.Body),
          $"{Translate("Successfully updated")} {Translate(typeof(T).Name)}"
        }
      },
      new DialogOptions { CloseOnEscapeKey = true });
  }

  private async Task OnDelete(T model)
  {
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
      else if (model is IAuditable auditable)
      {
        var mutations = ScopedServices.GetRequiredService<AuditableMutations>();
        await mutations.Delete(auditable, CancellationToken);
      }
      else
      {
        throw new InvalidOperationException(
          $"No delete strategy found for {typeof(T).Name}");
      }
    }
    catch (Exception ex)
    {
      await DialogService.ShowAsync<MutatingResult>(
        Translate("Failure"),
        new DialogParameters
        {
          {
            nameof(MutatingResult.Body),
            $"{Translate("Failed deleting")}"
            + $" {Translate(typeof(T).Name)} - {ex.Message}"
          }
        },
        new DialogOptions { CloseOnEscapeKey = true });
      return;
    }

    await DialogService.ShowAsync<MutatingResult>(
      Translate("Success"),
      new DialogParameters
      {
        {
          nameof(MutatingResult.Body),
          $"{Translate("Successfully deleted")} {Translate(typeof(T).Name)}"
        }
      },
      new DialogOptions { CloseOnEscapeKey = true });
  }

  private async Task OnRestore(T model)
  {
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
      else if (model is IAuditable auditable)
      {
        var mutations = ScopedServices.GetRequiredService<AuditableMutations>();
        await mutations.Restore(auditable, CancellationToken);
      }
      else
      {
        throw new InvalidOperationException(
          $"No restore strategy found for {typeof(T).Name}");
      }
    }
    catch (Exception ex)
    {
      await DialogService.ShowAsync<MutatingResult>(
        Translate("Failure"),
        new DialogParameters
        {
          {
            nameof(MutatingResult.Body),
            $"{Translate("Failed restoring")}"
            + $" {Translate(typeof(T).Name)} - {ex.Message}"
          }
        },
        new DialogOptions { CloseOnEscapeKey = true });
      return;
    }

    await DialogService.ShowAsync<MutatingResult>(
      Translate("Success"),
      new DialogParameters
      {
        {
          nameof(MutatingResult.Body),
          $"{Translate("Successfully restored")} {Translate(typeof(T).Name)}"
        }
      },
      new DialogOptions { CloseOnEscapeKey = true });
  }

  private async Task OnForget(T model)
  {
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
      else if (model is IAuditable auditable)
      {
        var mutations = ScopedServices.GetRequiredService<AuditableMutations>();
        await mutations.Forget(auditable, CancellationToken);
      }
      else
      {
        throw new InvalidOperationException(
          $"No forget strategy found for {typeof(T).Name}");
      }
    }
    catch (Exception ex)
    {
      await DialogService.ShowAsync<MutatingResult>(
        Translate("Failure"),
        new DialogParameters
        {
          {
            nameof(MutatingResult.Body),
            $"{Translate("Failed forgetting")}"
            + $" {Translate(typeof(T).Name)} - {ex.Message}"
          }
        },
        new DialogOptions { CloseOnEscapeKey = true });
      return;
    }

    await DialogService.ShowAsync<MutatingResult>(
      Translate("Success"),
      new DialogParameters
      {
        {
          nameof(MutatingResult.Body),
          $"{Translate("Successfully forgotten")} {Translate(typeof(T).Name)}"
        }
      },
      new DialogOptions { CloseOnEscapeKey = true });
  }
}
