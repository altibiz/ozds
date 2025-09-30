using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class StateModel : Model, IState
{
  public string CurrentState { get; set; } = default!;
}
