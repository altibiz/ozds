using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class StateModel : IStateModel
{
  public string CurrentState { get; set; } = default!;

  public IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    yield break;
  }
}
