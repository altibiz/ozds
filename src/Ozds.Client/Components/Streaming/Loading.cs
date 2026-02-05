using Microsoft.AspNetCore.Components;
using Ozds.Business.Activation;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Streaming;

public class Loading<T> : MappedLoading<T, T>
  where T : notnull { }

public partial class MappedLoading<T, TMapped> : OzdsComponentBase
  where T : notnull
{
  private Type? _activationType;
  private LoadingState<T> _state = new();

  [Parameter]
  public T? Value { get; set; }

  [Parameter]
  public Func<T, TMapped>? Map { get; set; }

  [Parameter]
  public RenderFragment? Progress { get; set; }

  [Parameter]
  public RenderFragment<string>? Error { get; set; }

  [Parameter]
  public RenderFragment? Concretize { get; set; }

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
  public string? JoinActivationId { get; set; }

  [Parameter]
  public Type? JoinActivationSide { get; set; }

  [Parameter]
  public bool WithTitle { get; set; }

  [Parameter]
  public bool WithHeading { get; set; }

  [Parameter]
  public RenderFragment? NotFound { get; set; }

  [Parameter]
  public RenderFragment<T>? Found { get; set; }

  [Parameter]
  public RenderFragment<T>? Created { get; set; }

  [Parameter]
  public string Class { get; set; } = default!;

  [Parameter]
  public string Style { get; set; } = default!;

  private List<Type> ActivatableSubtypes
  {
    get
    {
      return ScopedServices
        .GetRequiredService<ModelActivator>()
        .ActivatableSubtypes(typeof(T));
    }
  }

  public async Task Fetch()
  {
    _state = _state.WithReset();
#pragma warning disable S6966 // Awaitable method should be used
    Reload();
#pragma warning restore S6966 // Awaitable method should be used
    await ReloadAsync();
  }

  protected override void OnInitialized()
  {
    _activationType = ActivatableSubtypes.FirstOrDefault();

    if (Value is not null)
    {
      _state = _state.WithValue(Value);
    }
  }

  protected override void OnParametersSet()
  {
    Reload();
  }

  protected override async Task OnParametersSetAsync()
  {
    await ReloadAsync();
  }

  private void Reload()
  {
    if (Value is not null)
    {
      _state = _state.WithValue(Value);
      return;
    }

    if (_state.Stage is not LoadingStage.Loading and not LoadingStage.Unfound)
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
        _state = _state.WithError(e.ToString());
      }
    }

    if (_state.Stage is not LoadingStage.Loading and not LoadingStage.Unfound)
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
        _state = _state.WithError(e.ToString());
      }
    }

    if (_state.Stage is not LoadingStage.Loading and not LoadingStage.Unfound)
    {
      return;
    }

    if (Activate)
    {
      try
      {
        var activator = ScopedServices.GetRequiredService<ModelActivator>();
        var created = (T)
          activator.ActivateDynamic(_activationType ?? typeof(T));
        if (
          created is IJoin join
          && JoinActivationSide != null
          && JoinActivationId != null
        )
        {
          join.ActivationSide = JoinActivationSide;
          join.ActivationId = JoinActivationId;
        }

        _state = _state.WithCreated(created);
      }
      catch (Exception e)
      {
        _state = _state.WithError(e.ToString());
      }
    }
  }

  private async Task ReloadAsync()
  {
    if (Value is not null)
    {
      _state = _state.WithValue(Value);
      return;
    }

    if (_state.Stage is not LoadingStage.Loading and not LoadingStage.Unfound)
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
        _state = _state.WithError(e.ToString());
      }
    }

    if (_state.Stage is not LoadingStage.Loading and not LoadingStage.Unfound)
    {
      return;
    }

    // NOTE: keep this here in case we want to filter by deleted
    if (Id is not null && typeof(T).IsAssignableTo(typeof(ITrackable)))
    {
      try
      {
        _state = _state.WithValue(
          (T?)
            await ScopedServices
              .GetRequiredService<TrackableQueries>()
              .ReadById(typeof(T), Id, CancellationToken)
        );
      }
      catch (Exception e)
      {
        _state = _state.WithError(e.ToString());
      }
    }

    if (Id is not null && typeof(T).IsAssignableTo(typeof(IIdentifiable)))
    {
      try
      {
        _state = _state.WithValue(
          (T?)
            await ScopedServices
              .GetRequiredService<IdentifiableQueries>()
              .ReadById(typeof(T), Id, CancellationToken)
        );
      }
      catch (Exception e)
      {
        _state = _state.WithError(e.ToString());
      }
    }

    if (_state.Stage is not LoadingStage.Loading and not LoadingStage.Unfound)
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
        _state = _state.WithError(e.ToString());
      }
    }

    if (_state.Stage is not LoadingStage.Loading and not LoadingStage.Unfound)
    {
      return;
    }

    if (ActivateAsync)
    {
      try
      {
        var activator = ScopedServices.GetRequiredService<ModelActivator>();
        var created = (T)
          activator.ActivateDynamic(_activationType ?? typeof(T));
        if (
          created is IJoin join
          && JoinActivationSide != null
          && JoinActivationId != null
        )
        {
          join.ActivationSide = JoinActivationSide;
          join.ActivationId = JoinActivationId;
        }

        _state = _state.WithCreated(created);
      }
      catch (Exception e)
      {
        _state = _state.WithError(e.ToString());
      }
    }
  }

  private void Reactivate(Type type)
  {
    if (!type.IsAssignableTo(typeof(T)))
    {
      throw new ArgumentException(
        $"Type {type} is not assignable to {typeof(T)}",
        nameof(type)
      );
    }

    _activationType = type;

    if (_state.Stage is not LoadingStage.Created)
    {
      return;
    }

    if (Activate)
    {
      try
      {
        var activator = ScopedServices.GetRequiredService<ModelActivator>();
        var created = (T)activator.ActivateDynamic(_activationType);
        if (
          created is IJoin join
          && JoinActivationSide != null
          && JoinActivationId != null
        )
        {
          join.ActivationSide = JoinActivationSide;
          join.ActivationId = JoinActivationId;
        }

        _state = _state.WithCreated(created);
      }
      catch (Exception e)
      {
        _state = _state.WithError(e.ToString());
      }
    }

    if (ActivateAsync)
    {
      try
      {
        var activator = ScopedServices.GetRequiredService<ModelActivator>();
        var created = (T)activator.ActivateDynamic(_activationType);
        if (
          created is IJoin join
          && JoinActivationSide != null
          && JoinActivationId != null
        )
        {
          join.ActivationSide = JoinActivationSide;
          join.ActivationId = JoinActivationId;
        }

        _state = _state.WithCreated(created);
      }
      catch (Exception e)
      {
        _state = _state.WithError(e.ToString());
      }
    }
  }
}
