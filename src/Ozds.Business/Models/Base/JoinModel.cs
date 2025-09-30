using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class JoinModel : Model, IJoin
{
  public string ActivationId
  {
    get { return ActivationSide == LeftType ? LeftId : RightId; }
    set
    {
      if (ActivationSide == LeftType)
      {
        LeftId = value;
      }
      else
      {
        RightId = value;
      }
    }
  }

  public Type ActivationSide { get; set; } = default!;

  public abstract string LeftId { get; set; }

  public abstract Type LeftType { get; }

  public abstract string RightId { get; set; }

  public abstract Type RightType { get; }
}
