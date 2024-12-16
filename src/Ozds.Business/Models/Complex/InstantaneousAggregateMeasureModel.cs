using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Complex;

public class InstantaneousAggregateMeasureModel : IAggregateMeasure
{
  [Required]
  public decimal Avg { get; set; } = default!;

  [Required]
  public decimal Min { get; set; } = default!;

  [Required]
  public decimal Max { get; set; } = default!;

  [Required]
  public DateTimeOffset MinTimestamp { get; set; } = default!;

  [Required]
  public DateTimeOffset MaxTimestamp { get; set; } = default!;
}
