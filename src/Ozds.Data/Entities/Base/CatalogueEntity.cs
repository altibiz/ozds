using System.ComponentModel.DataAnnotations.Schema;

namespace Ozds.Data.Entities.Base;

[Table("catalogues")]
public abstract class CatalogueEntity : AuditableEntity
{
  [ForeignKey(nameof(Location))]
  public string LocationId { get; set; } = default!;

  public virtual LocationEntity Location { get; set; } = default!;
}
