using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class LocationModel : AuditableModel
{
  [Required]
  public required string WhiteMediumNetworkUserCatalogueId { get; set; }

  [Required] public required string BlueLowNetworkUserCatalogueId { get; set; }

  [Required] public required string WhiteLowNetworkUserCatalogueId { get; set; }

  [Required] public required string RedLowNetworkUserCatalogueId { get; set; }

  [Required] public required string RegulatoryCatalogueId { get; set; }

  public static LocationModel New()
  {
    return new LocationModel
    {
      Id = default!,
      Title = "",
      CreatedOn = DateTimeOffset.UtcNow,
      CreatedById = default,
      LastUpdatedOn = default,
      LastUpdatedById = default,
      IsDeleted = false,
      DeletedOn = default,
      DeletedById = default,
      WhiteMediumNetworkUserCatalogueId = default!,
      BlueLowNetworkUserCatalogueId = default!,
      WhiteLowNetworkUserCatalogueId = default!,
      RedLowNetworkUserCatalogueId = default!,
      RegulatoryCatalogueId = default!
    };
  }
}
