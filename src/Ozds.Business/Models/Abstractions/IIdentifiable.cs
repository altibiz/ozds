namespace Ozds.Business.Models.Abstractions;

public interface IIdentifiable : IModel
{
  string Title { get; }

  string Id { get; }
}
