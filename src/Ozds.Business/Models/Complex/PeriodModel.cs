using System.ComponentModel.DataAnnotations;
using System.Text;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Complex;

public class PeriodModel : IModel, IValidatableObject
{
  public DurationModel Duration { get; set; }

  public uint Multiplier { get; set; }

  public IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    yield break;
  }
}

public static class PeriodModelExtensions
{
  public static string ToTitle(this PeriodModel model)
  {
    var builder = new StringBuilder();

    if (model.Multiplier > 1)
    {
      builder.Append($"{model.Multiplier} ");
    }
    else
    {
      builder.Append("a ");
    }

    builder.Append(model.Duration.ToTitle(model.Multiplier > 1));

    return builder.ToString();
  }

  public static TimeSpan ToTimeSpan(this PeriodModel model)
  {
    return model.Duration.ToTimeSpan() * model.Multiplier;
  }
}
