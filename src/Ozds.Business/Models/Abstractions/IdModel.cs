using System.ComponentModel.DataAnnotations;

namespace Ozds.Business.Models.Abstractions;

public abstract record IdModel(string Id) : IValidatableObject
{
  public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
}
