namespace Ozds.Data.Entities.Abstractions;

public interface IJoinEntity : IEntity
{
  public string LeftId { get; }

  public string RightId { get; }
}
