namespace Ozds.Business.Models.Abstractions;

public interface IStateModel : IModel
{
  public string CurrentState { get; }
}
