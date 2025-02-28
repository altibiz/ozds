using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Complex;

public class AggregateMeasureModel : IAggregateMeasure
{
  [Required]
  public decimal Min { get; set; } = default!;

  [Required]
  public decimal Max { get; set; } = default!;
}
