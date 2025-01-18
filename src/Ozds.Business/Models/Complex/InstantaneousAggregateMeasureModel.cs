using System.ComponentModel.DataAnnotations;

namespace Ozds.Business.Models.Complex;

public class InstantaneousAggregateMeasureModel : AggregateMeasureModel
{
  [Required]
  public decimal Avg { get; set; } = default!;

  [Required]
  public DateTimeOffset MinTimestamp { get; set; } = default!;

  [Required]
  public DateTimeOffset MaxTimestamp { get; set; } = default!;
}
