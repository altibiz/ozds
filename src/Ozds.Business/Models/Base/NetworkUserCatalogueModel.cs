using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

// NOTE: don't make this thing abstract for now - archived properties break

public class NetworkUserCatalogueModel
  : CatalogueModel, INetworkUserCatalogue
{
  [Required]
  [Range(0, uint.MaxValue)]
  public required decimal MeterFeePrice_EUR { get; set; }
}
