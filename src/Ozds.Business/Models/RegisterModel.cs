using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models;

public class RegisterModel : TrackableModel
{
  [Required]
  public required string ScopeId { get; set; } = default!;

  [Required]
  public required string Name { get; set; } = default!;

  [Required]
  public required MeasureModel Measure { get; set; } = default!;

  public OrderOfMagnitudeModel? OrderOfMagnitude { get; set; } = default!;

  public TariffModel? Tariff { get; set; } = default!;

  public DuplexModel? Duplex { get; set; } = default!;

  public PhaseModel? Phase { get; set; } = default!;

  public AggregationModel? Aggregation { get; set; } = default!;
}
