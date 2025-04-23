namespace Ozds.Report.Entities;

public class NetworkUserMeasurementLocationEntity : IdentifiableEntity
{
  public string MeterId { get; set; } = default!;

  public string NetworkUserId { get; set; } = default!;

  public string NetworkUserCatalogueId { get; set; } = default!;
}
