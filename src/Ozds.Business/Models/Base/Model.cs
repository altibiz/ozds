using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class Model : IModel
{
  public bool Created { get; set; } = false;

  public virtual IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext
  )
  {
    return Enumerable.Empty<ValidationResult>();
  }
}
