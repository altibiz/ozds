namespace Ozds.Client.Records;

public class NetworkUserMeasurementLocationRecord : IdentifiableRecord
{
  public string MeterId { get; set; } = default!;

  public string NetworkUserId { get; set; } = default!;

  public string NetworkUserCatalogueId { get; set; } = default!;
}
