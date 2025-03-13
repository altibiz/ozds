using System.ComponentModel.DataAnnotations;

namespace Ozds.Business.Models.Complex;

public class DerivedAggregateMeasureModel : AggregateMeasureModel
{
  [Required]
  public decimal Min { get; set; } = default!;

  [Required]
  public decimal Max { get; set; } = default!;

  [Required]
  public decimal Avg { get; set; } = default!;

  [Required]
  public DateTimeOffset MinTimestamp { get; set; } = default!;

  [Required]
  public DateTimeOffset MaxTimestamp { get; set; } = default!;
}
