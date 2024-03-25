using System.ComponentModel.DataAnnotations;

namespace Ozds.Data.Entities.Base;

public abstract class IdentifiableEntity
{
  [Key] public string Id { get; set; } = default!;

  public string Title { get; set; } = default!;
}
