using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class IdentifiableModel : Model, IIdentifiable
{
  public required string Id { get; set; }

  [Required]
  public required string Title { get; set; }
}
