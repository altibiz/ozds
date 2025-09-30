using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Joins;

public class ApiKeyScopeModel : AuditableJoinModel
{
  public override string LeftId
  {
    get { return ApiKeyId; }
    set { ApiKeyId = value; }
  }

  public override Type LeftType
  {
    get { return typeof(ApiKeyModel); }
  }

  public override string RightId
  {
    get { return ScopeId; }
    set { ScopeId = value; }
  }

  public override Type RightType
  {
    get { return typeof(ScopeModel); }
  }

  [Required]
  public required string ApiKeyId { get; set; } = default!;

  [Required]
  public required string ScopeId { get; set; } = default!;
}
