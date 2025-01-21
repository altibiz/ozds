using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Ozds.Business.Activation;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Streaming;

public partial class Loading<T> : OzdsOwningComponentBase
  where T : notnull
{
  [Parameter]
  public T? Value { get; set; }

  [Parameter]
  public RenderFragment? Progress { get; set; }

  [Parameter]
  public RenderFragment<string>? Error { get; set; }

  [Parameter]
  public string? Id { get; set; }

  [Parameter]
  public Func<T?>? Load { get; set; }

  [Parameter]
  public Func<Task<T?>>? LoadAsync { get; set; }

  [Parameter]
  public Func<T>? New { get; set; }

  [Parameter]
  public Func<Task<T>>? NewAsync { get; set; }

  [Parameter]
  public bool Activate { get; set; }

  [Parameter]
  public bool ActivateAsync { get; set; }

  [Parameter]
  public RenderFragment? NotFound { get; set; }

  [Parameter]
  public RenderFragment<T>? Found { get; set; }

  [Parameter]
  public RenderFragment<T>? Created { get; set; }

  private LoadingState<T> _state = new();

  public void Initialize(
    bool reset = false,
    bool render = false
  )
  {
    if (reset)
    {
      _state = _state.WithReset();
    }

    if (Value is not null)
    {
      _state = _state.WithValue(Value);
    }

    if (render)
    {
      InvokeAsync(StateHasChanged);
    }
  }

  public void Reload(
    bool reset = false,
    bool render = false
  )
  {
    if (reset)
    {
      _state = _state.WithReset();
    }

    if (Value is not null)
    {
      _state = _state.WithValue(Value);
      return;
    }

    if (_state.State is not LoadingState.Loading and not LoadingState.Unfound)
    {
      return;
    }

    if (Load is not null)
    {
      try
      {
        _state = _state.WithValue(Load());
      }
      catch (Exception e)
      {
        _state = _state.WithError(e.Message);
      }
    }

    if (_state.State is not LoadingState.Loading and not LoadingState.Unfound)
    {
      return;
    }

    if (New is not null)
    {
      try
      {
        _state = _state.WithCreated(New());
      }
      catch (Exception e)
      {
        _state = _state.WithError(e.Message);
      }
    }

    if (_state.State is not LoadingState.Loading and not LoadingState.Unfound)
    {
      return;
    }

    if (Activate)
    {
      try
      {
        var activator = ScopedServices
          .GetRequiredService<ModelActivator>();
        var created = activator.Activate<T>();
        _state = _state.WithCreated(created);
      }
      catch (Exception e)
      {
        _state = _state.WithError(e.Message);
      }
    }

    if (render)
    {
      InvokeAsync(StateHasChanged);
    }
  }

  public async Task ReloadAsync(
    bool reset = false,
    bool render = false
  )
  {
    await Task.Run(() => { });

    if (reset)
    {
      _state = _state.WithReset();
    }

    if (Value is not null)
    {
      _state = _state.WithValue(Value);
      return;
    }

    if (_state.State is not LoadingState.Loading and not LoadingState.Unfound)
    {
      return;
    }

    if (LoadAsync is not null)
    {
      try
      {
        _state = _state.WithValue(await LoadAsync());
      }
      catch (Exception e)
      {
        _state = _state.WithError(e.Message);
      }
    }

    if (_state.State is not LoadingState.Loading and not LoadingState.Unfound)
    {
      return;
    }

    if (Id is not null && typeof(T).IsAssignableTo(typeof(IAuditable)))
    {
      try
      {
        _state = _state.WithValue(
          (T?)await ScopedServices
            .GetRequiredService<AuditableQueries>()
            .ReadSingleDynamic(typeof(T), Id, CancellationToken));
      }
      catch (Exception e)
      {
        _state = _state.WithError(e.Message);
      }
    }

    if (Id is not null && typeof(T).IsAssignableTo(typeof(IReadonly)))
    {
      try
      {
        _state = _state.WithValue(
          (T?)await ScopedServices
            .GetRequiredService<ReadonlyQueries>()
            .ReadSingleDynamic(typeof(T), Id, CancellationToken));
      }
      catch (Exception e)
      {
        _state = _state.WithError(e.Message);
      }
    }

    if (_state.State is not LoadingState.Loading and not LoadingState.Unfound)
    {
      return;
    }

    if (NewAsync is not null)
    {
      try
      {
        _state = _state.WithCreated(await NewAsync());
      }
      catch (Exception e)
      {
        _state = _state.WithError(e.Message);
      }
    }

    if (_state.State is not LoadingState.Loading and not LoadingState.Unfound)
    {
      return;
    }

    if (ActivateAsync)
    {
      try
      {
        var activator = ScopedServices
          .GetRequiredService<ModelActivator>();
        var created = activator.Activate<T>();
        _state = _state.WithCreated(created);
      }
      catch (Exception e)
      {
        _state = _state.WithError(e.Message);
      }
    }

    if (render)
    {
      await InvokeAsync(StateHasChanged);
    }
  }

  protected override void OnInitialized()
  {
    Initialize();
  }

  protected override void OnParametersSet()
  {
    Reload();
  }

  protected override async Task OnParametersSetAsync()
  {
    await ReloadAsync();
  }
}
