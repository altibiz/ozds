namespace Ozds.Client.Records;

public class SchneideriEM3xxxMeterRecord : IdentifiableRecord
{
  public string Id { get; set; } = default!;

  public string MeasurementValidatorId { get; set; } = default!;

  public string MessengerId { get; set; } = default!;

  public decimal ConnectionPower_W { get; set; } = default!;

  public string Phases { get; set; } = default!;
}
