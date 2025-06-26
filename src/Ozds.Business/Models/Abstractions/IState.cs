namespace Ozds.Business.Models.Abstractions;

public interface IState : IModel
{
  public string CurrentState { get; }
}
