using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Data.Entities.Complex;

public class AggregateMeasureEntity : IAggregateMeasureEntity
{
}

public static class AggregateMeasureEntityExtensions
{
#pragma warning disable IDE0060 // Remove unused parameter
  public static void AggregateMeasure(
    this ComplexPropertyBuilder builder,
    string name,
    string unit
  )
  {
    // NOTE: left here for reference
  }
#pragma warning restore IDE0060 // Remove unused parameter
}
