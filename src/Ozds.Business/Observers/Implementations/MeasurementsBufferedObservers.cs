using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.Base;
using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers.Implementations;

public class MeasurementsBufferedObservers
  : Observer<MeasurementsBufferedEventArgs>,
    IMeasurementsBufferedPublisher,
    IMeasurementsBufferedSubscriber
{
}
