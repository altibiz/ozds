using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class JoinModel : IJoin
{
  public virtual IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    yield break;
  }
}
