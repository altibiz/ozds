using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ozds.Data.Entities.Base;

[NotMapped]
public abstract class IdentifiableEntity
{
  [Key]
  [Column(TypeName = "bigint")]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public string Id { get; set; } = default!;

  public string Title { get; set; } = default!;
}
