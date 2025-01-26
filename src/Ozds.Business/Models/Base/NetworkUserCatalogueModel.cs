using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public class NetworkUserCatalogueModel
  : CatalogueModel, INetworkUserCatalogue
{
  [Required]
  [Range(0, uint.MaxValue)]
  public required decimal MeterFeePrice_EUR { get; set; }
}
