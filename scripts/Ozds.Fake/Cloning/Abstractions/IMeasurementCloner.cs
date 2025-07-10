using Ozds.Business.Models.Abstractions;

namespace Ozds.Fake.Cloning.Abstractions;

public interface IMeasurementCloner
{
  public bool CanClone(Type type);

  public IMeasurement Clone(IMeasurement measurement);
}
