using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class NetworkUserMeasurementLocationModel : MeasurementLocationModel
{
  [Required]
  public required string NetworkUserId { get; set; }

  [Required]
  public required string NetworkUserCatalogueId { get; set; }
}
