namespace Ozds.Data.Entities.Abstractions;

public interface IIdentifiableEntity : IEntity
{
  string Id { get; }

  string Title { get; }
}
