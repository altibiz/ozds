using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Complex;

public class PeriodModel : Model
{
  [Required]
  public required DurationModel Duration { get; set; }

  [Required]
  public required uint Multiplier { get; set; }
}
