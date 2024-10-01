using System.ComponentModel.DataAnnotations;

namespace Ozds.Business.Models.Abstractions;

public interface IIdentifiable : IValidatableObject, IModel
{
  string Title { get; }

  string Id { get; }
}
