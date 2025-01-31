namespace Ozds.Data.Entities.Composite;

public class NetworkUserInvoiceIssuingBasisEntity
{
  public LocationEntity Location { get; set; } = default!;

  public NetworkUserEntity NetworkUser { get; set; } = default!;

  public RegulatoryCatalogueEntity RegulatoryCatalogue { get; set; } =
    default!;

  public DateTimeOffset FromDate { get; set; }

  public DateTimeOffset ToDate { get; set; }

  public List<NetworkUserCalculationBasisEntity>
    NetworkUserCalculationBases { get; set; } = default!;
}
