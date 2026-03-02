using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Composite;

public class MaybeRepresentingUserModel : IComposite, IValidatableObject
{
  public UserModel User { get; set; } = default!;

  // NOTE: only used during creation of a new account
  public NewPasswordModel? NewPassword { get; set; } = default!;

  public RepresentativeModel? Representative { get; set; } = default!;

  public IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext
  )
  {
    var userContext = new ValidationContext(
      User,
      validationContext,
      validationContext.Items
    );
    var userResults = new List<ValidationResult>();
    Validator.TryValidateObject(User, userContext, userResults, true);

    foreach (var r in userResults)
    {
      yield return r;
    }

    if (NewPassword is { } pass)
    {
      var passContext = new ValidationContext(
        pass,
        validationContext,
        validationContext.Items
      );
      var passResults = new List<ValidationResult>();
      Validator.TryValidateObject(pass, passContext, passResults, true);
      foreach (var r in passResults)
      {
        yield return r;
      }
    }

    if (Representative is { } rep)
    {
      var repContext = new ValidationContext(
        rep,
        validationContext,
        validationContext.Items
      );
      var repResults = new List<ValidationResult>();
      Validator.TryValidateObject(rep, repContext, repResults, true);
      foreach (var r in repResults)
      {
        yield return r;
      }
    }
  }
}
