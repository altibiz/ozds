using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Data.Entities.Complex;

public class AggregateMeasureEntity : IAggregateMeasureEntity
{
}

public static class AggregateMeasureEntityExtensions
{
  public static void AggregateMeasure(
    this ComplexPropertyBuilder builder,
    string name,
    string unit
  )
  {
    // NOTE: left here for reference
  }
}
