using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class IdentifiableModel : IIdentifiable
{
  public required string Id { get; init; }

  [Required] public required string Title { get; set; }

  public virtual IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    return Enumerable.Empty<ValidationResult>();
  }
}
