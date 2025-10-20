using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models;

public class ScopeModel : TrackableModel, IScope
{
  public string? ScopeModelId { get; set; }

  public string? ScopeModelType { get; set; }

  [Required]
  public required ActionModel ScopeAction { get; set; } = default!;
}
