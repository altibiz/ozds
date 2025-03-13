using System.ComponentModel.DataAnnotations;

namespace Ozds.Business.Models.Complex;

public class CumulativeAggregateMeasureModel : AggregateMeasureModel
{
  [Required]
  public decimal Min { get; set; } = default!;

  [Required]
  public decimal Max { get; set; } = default!;
}
