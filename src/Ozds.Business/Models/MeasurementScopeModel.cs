using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models;

public class MeasurementScopeModel : ScopeModel
{
  [Required]
  public required IntervalModel Interval { get; set; } = default!;
}
