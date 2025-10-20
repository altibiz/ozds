namespace Ozds.Business.Models.Abstractions;

public interface IJoin : IModel
{
  public string ActivationId { get; set; }

  public Type ActivationSide { get; set; }

  public string LeftId { get; set; }

  public Type LeftType { get; }

  public string RightId { get; set; }

  public Type RightType { get; }
}
