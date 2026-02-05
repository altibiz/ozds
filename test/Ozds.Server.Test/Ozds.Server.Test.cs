using Ozds.Server.Test;
using TUnit.Core.Interfaces;

// NOTE: this makes e2e tests much more reliable

#pragma warning disable SA1015 // Closing generic brackets should be spaced correctly
[assembly: ParallelLimiter<OzdsServerTestParallelLimiter>]

#pragma warning restore SA1015 // Closing generic brackets should be spaced correctly

#pragma warning disable S3261
namespace Ozds.Server.Test;

#pragma warning restore S3261

public sealed class OzdsServerTestParallelLimiter : IParallelLimit
{
  public int Limit
  {
    get { return 1; }
  }
}
