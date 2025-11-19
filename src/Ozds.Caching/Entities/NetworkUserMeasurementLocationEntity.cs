using Ozds.Caching.Entities.Base;

namespace Ozds.Caching.Entities;

public class NetworkUserMeasurementLocationEntity : MeasurementLocationEntity
{
  public string NetworkUserId { get; set; } = default!;

  public string NetworkUserCatalogueId { get; set; } = default!;

  public string CalculationRemark { get; set; } = default!;
}
