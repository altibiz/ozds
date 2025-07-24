using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Complex;
using Ozds.Business.Naming;

namespace Ozds.Business.Models.Base;

public class MessengerModel : AuditableModel, IMessenger
{
  [Required]
  public required string LocationId { get; set; }

  [Required]
  public required PeriodModel MaxInactivityPeriod { get; set; }

  [Required]
  public required PeriodModel PushDelayPeriod { get; set; }

  public override IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    foreach (var validationResult in base.Validate(validationContext))
    {
      yield return validationResult;
    }

    if (validationContext.MemberName is null or nameof(Id))
    {
      if (Id is null)
      {
        yield return new ValidationResult(
          "ID must be set",
          new[] { nameof(Id) });
      }
      else
      {
        var convention = validationContext
          .GetRequiredService<MessengerNamingConvention>();

        ValidationResult? validationResult = null;
        try
        {
          var expectedType = convention.MessengerTypeForMessengerId(Id);
          var actualType = GetType();
          if (expectedType != actualType)
          {
            validationResult = new ValidationResult(
              $"Unconventional messenger ID {Id} for {actualType}",
              new[] { nameof(Id) });
          }
        }
        catch (Exception)
        {
          validationResult = new ValidationResult(
            $"Unconventional messenger ID {Id}",
            new[] { nameof(Id) });
        }

        if (validationResult is not null)
        {
          yield return validationResult;
        }
      }
    }
  }
}
