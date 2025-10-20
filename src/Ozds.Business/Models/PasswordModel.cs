using System.ComponentModel.DataAnnotations;

namespace Ozds.Business.Models;

public class PasswordModel : Base.UserModel
{
  [Required]
  public required string UserId { get; set; }

  [Required]
  public required string OldPassword { get; set; }

  [Required]
  public required string NewPassword { get; set; }

  [Required]
  public required string ConfirmNewPassword { get; set; }

  public override IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext
  )
  {
    if (
      (validationContext.MemberName is null ||
        validationContext.MemberName == nameof(ConfirmNewPassword))
      && NewPassword != ConfirmNewPassword)
    {
      yield return new ValidationResult(
        "Passwords do not match.",
        new[] { nameof(ConfirmNewPassword) });
    }

    if (validationContext.MemberName is null
      || validationContext.MemberName == nameof(NewPassword))
    {
      if (NewPassword.Length < 8)
      {
        yield return new ValidationResult(
          "Password must be at least 8 characters long.",
          new[] { nameof(NewPassword) }
        );
      }

      if (NewPassword.Length > 128)
      {
        yield return new ValidationResult(
          "Password must be at most 128 characters long.",
          new[] { nameof(NewPassword) }
        );
      }

      if (!NewPassword.All(char.IsAscii))
      {
        yield return new ValidationResult(
          "Password must be ASCII characters only.",
          new[] { nameof(NewPassword) }
        );
      }

      if (!NewPassword.All(
        @char =>
          char.IsLetter(@char)
          || char.IsDigit(@char)
          || char.IsSymbol(@char)
          || char.IsPunctuation(@char)))
      {
        yield return new ValidationResult(
          "Password characters must be letters, digits, symbols or punctuation.",
          new[] { nameof(NewPassword) }
        );
      }

      if (!NewPassword.Any(char.IsLetter))
      {
        yield return new ValidationResult(
          "Password must contain at least one letter.",
          new[] { nameof(NewPassword) }
        );
      }

      if (!NewPassword.Any(char.IsDigit))
      {
        yield return new ValidationResult(
          "Password must contain at least one digit.",
          new[] { nameof(NewPassword) }
        );
      }

      if (!NewPassword.Any(
        @char =>
          char.IsSymbol(@char)
          || char.IsPunctuation(@char)))
      {
        yield return new ValidationResult(
          "Password must contain at least one symbol or punctuation.",
          new[] { nameof(NewPassword) }
        );
      }
    }
  }
}
