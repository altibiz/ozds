using Ozds.Business.Naming.Abstractions;

namespace Ozds.Business.Naming.Base;

#pragma warning disable S1694 // An abstract class should have both abstract and concrete methods
public abstract class ConcreteMessengerNamingConvention
  : IMessengerNamingConvention
#pragma warning restore S1694 // An abstract class should have both abstract and concrete methods
{
  public abstract string IdPrefix { get; }

  public abstract Type MessengerType { get; }
}
