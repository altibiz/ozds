using Quartz;

namespace Ozds.Jobs.Jobs.Abstractions;

public interface IJobInstantiator : IJob
{
  public Task Instantiate();
}
