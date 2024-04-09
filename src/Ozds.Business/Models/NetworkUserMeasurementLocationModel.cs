using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class NetworkUserMeasurementLocationModel : MeasurementLocationModel
{
  [Required] public required string NetworkUserId { get; set; }
  [Required] public required string CatalogueId { get; set; }
  public static NetworkUserMeasurementLocationModel New()
  {
    return new NetworkUserMeasurementLocationModel
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
      NetworkUserId = default!,
      CatalogueId = default!,
      MeterId = default!,
    };
  }
}
