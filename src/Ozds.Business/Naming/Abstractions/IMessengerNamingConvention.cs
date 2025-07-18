namespace Ozds.Business.Naming.Abstractions;

public interface IMessengerNamingConvention
{
  public string IdPrefix { get; }
  public Type MessengerType { get; }
}
