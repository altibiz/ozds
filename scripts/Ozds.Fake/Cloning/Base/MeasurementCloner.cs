using Ozds.Business.Models.Abstractions;
using Ozds.Fake.Cloning.Abstractions;

namespace Ozds.Fake.Cloning.Base;

public abstract class MeasurementCloner<T> : IMeasurementCloner
  where T : IMeasurement
{
  public bool CanClone(Type type)
  {
    return type == typeof(T);
  }

  public IMeasurement Clone(IMeasurement measurement)
  {
    return Clone((T)measurement);
  }

  protected abstract T Clone(T measurement);
}
