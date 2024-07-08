using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class LocationMeasurementLocationModel : MeasurementLocationModel
{
  [Required]
  public required string LocationId { get; set; }

  public static LocationMeasurementLocationModel New()
  {
    return new LocationMeasurementLocationModel
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
      LocationId = default!,
      MeterId = default!
    };
  }
}
