using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract record IdentifiableModel(string Id, string Title) : IIdentifiable
{
  public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
}
