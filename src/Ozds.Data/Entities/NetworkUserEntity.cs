using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class NetworkUserEntity : SoftDeletableEntity
{
  public virtual List<RepresentativeEntity> Representatives { get; set; } = default!;

  public virtual LocationEntity Location { get; set; } = default!;

  public virtual List<NetworkUserMeasurementLocationEntity> MeasurementLocations { get; set; } = default!;

  public virtual List<NetworkUserInvoiceEntity> Invoices { get; set; } = default!;
}
