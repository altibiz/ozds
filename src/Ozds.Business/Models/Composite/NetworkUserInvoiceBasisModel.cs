using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Composite;

public class NetworkUserInvoiceBasisModel : IComposite
{
  public LocationModel Location { get; set; } = default!;

  public NetworkUserModel NetworkUser { get; set; } = default!;

  public RegulatoryCatalogueModel RegulatoryCatalogue { get; set; } =
    default!;

  public DateTimeOffset FromDate { get; set; }

  public DateTimeOffset ToDate { get; set; }

  public List<NetworkUserCalculationBasisModel>
    NetworkUserCalculationBases { get; set; } = default!;
}
