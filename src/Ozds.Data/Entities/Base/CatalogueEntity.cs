using System.ComponentModel.DataAnnotations.Schema;

namespace Ozds.Data.Entities.Base;

[Table("catalogues")]
public abstract class CatalogueEntity : IdEntity
{
  public virtual LocationEntity Location { get; set; } = default!;
}
