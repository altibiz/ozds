namespace Ozds.Business.Models.Abstractions;

public interface IIdentifiable : IModel
{
  string Title { get; set; }

  string Id { get; }
}
