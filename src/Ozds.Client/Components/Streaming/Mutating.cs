using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations.Agnostic;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Streaming;

// TODO: memoize Loading parameters to prevent rerendering

public partial class Mutating<T> : OzdsOwningComponentBase
  where T : notnull
{
  [Parameter]
  public T? Value { get; set; }

  [Parameter]
  public string? Id { get; set; } = default!;

  [Parameter]
  public RenderFragment? Progress { get; set; }

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
  public RenderFragment<MutatingState<T>>? Label { get; set; } = default!;

  [Parameter]
  public RenderFragment<MutatingState<T>> Details { get; set; } = default!;

  [Parameter]
  public RenderFragment<MutatingState<T>> Edit { get; set; } = default!;

  private bool _mutating;

  private async Task OnCreate(T model)
  {
    var dialogService = ScopedServices.GetRequiredService<IDialogService>();

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
      else if (model?.GetType().IsAssignableTo(typeof(IAuditable)) ?? false)
      {
        var mutations = ScopedServices.GetRequiredService<AuditableMutations>();
        await mutations.Create((model as IAuditable)!, CancellationToken.None);
      }
      else
      {
        throw new InvalidOperationException(
          $"No create strategy found for {typeof(T).Name}");
      }
    }
    catch (Exception ex)
    {
      await dialogService.ShowAsync<MutatingResult>(
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

    await dialogService.ShowAsync<MutatingResult>(
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
    var dialogService = ScopedServices.GetRequiredService<IDialogService>();

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
      else if (model?.GetType().IsAssignableTo(typeof(IAuditable)) ?? false)
      {
        var mutations = ScopedServices.GetRequiredService<AuditableMutations>();
        await mutations.Update((model as IAuditable)!, CancellationToken.None);
      }
      else
      {
        throw new InvalidOperationException(
          $"No update strategy found for {typeof(T).Name}");
      }
    }
    catch (Exception ex)
    {
      await dialogService.ShowAsync<MutatingResult>(
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

    await dialogService.ShowAsync<MutatingResult>(
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
    var dialogService = ScopedServices.GetRequiredService<IDialogService>();

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
      else if (model?.GetType().IsAssignableTo(typeof(IAuditable)) ?? false)
      {
        var mutations = ScopedServices.GetRequiredService<AuditableMutations>();
        await mutations.Delete((model as IAuditable)!, CancellationToken.None);
      }
      else
      {
        throw new InvalidOperationException(
          $"No delete strategy found for {typeof(T).Name}");
      }
    }
    catch (Exception ex)
    {
      await dialogService.ShowAsync<MutatingResult>(
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

    await dialogService.ShowAsync<MutatingResult>(
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
}
