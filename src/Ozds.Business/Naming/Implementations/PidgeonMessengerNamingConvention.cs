using Ozds.Business.Models;
using Ozds.Business.Naming.Base;

namespace Ozds.Business.Naming.Implementations;

public class PidgeonMessengerNamingConvention
  : ConcreteMessengerNamingConvention
{
  public override string IdPrefix { get; } = "pidgeon";

  public override Type MessengerType { get; } = typeof(PidgeonMessengerModel);
}
