using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class RepresentativeAuditEventModel : AuditEventModel
{
  [Required] public required string RepresentativeId { get; set; }
}
