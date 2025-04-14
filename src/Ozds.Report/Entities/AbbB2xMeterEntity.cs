using Ozds.Report.Entities.Abstractions;

namespace Ozds.Report.Entities;

public class AbbB2xMeterEntity : IdentifiableEntity, IMeterEntity
{
  public string Id { get; set; } = default!;

  public string MeasurementValidatorId { get; set; } = default!;

  public string MessengerId { get; set; } = default!;

  public decimal ConnectionPower_W { get; set; } = default!;

  public string Phases { get; set; } = default!;
}
