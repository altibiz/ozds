using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class LocationModel : AuditableModel
{
  [Required] public required string WhiteMediumCatalogueId { get; set; }

  [Required] public required string BlueLowCatalogueId { get; set; }

  [Required] public required string WhiteLowCatalogueId { get; set; }

  [Required] public required string RedLowCatalogueId { get; set; }

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
      WhiteMediumCatalogueId = default!,
      BlueLowCatalogueId = default!,
      WhiteLowCatalogueId = default!,
      RedLowCatalogueId = default!,
      RegulatoryCatalogueId = default!,
    };
  }
}
