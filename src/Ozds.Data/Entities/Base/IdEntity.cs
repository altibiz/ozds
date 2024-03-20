using System.ComponentModel.DataAnnotations;

namespace Ozds.Data.Entities.Base;

public abstract class IdEntity
{
  [Key]
  public string Id { get; set; } = default!;
}
