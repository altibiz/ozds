using System.ComponentModel.DataAnnotations;

namespace Ozds.Business.Models.Abstractions;

public interface IIdentifiable : IValidatableObject
{
  string Title { get; }

  string Id { get; }
}
