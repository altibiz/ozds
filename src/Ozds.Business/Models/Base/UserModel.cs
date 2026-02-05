using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class UserModel : IUser
{
  public virtual IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext
  )
  {
    return Enumerable.Empty<ValidationResult>();
  }
}
