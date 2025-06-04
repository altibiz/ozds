using Ozds.Client.Test;
using TUnit.Core.Interfaces;

// NOTE: this makes e2e tests much more reliable

#pragma warning disable SA1015 // Closing generic brackets should be spaced correctly
[assembly: ParallelLimiter<OzdsClientTestParallelLimiter>]
#pragma warning restore SA1015 // Closing generic brackets should be spaced correctly

#pragma warning disable S3261
namespace Ozds.Client.Test;
#pragma warning restore S3261

public sealed class OzdsClientTestParallelLimiter : IParallelLimit
{
  public int Limit
  {
    get { return 1; }
  }
}
