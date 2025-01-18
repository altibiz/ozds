using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Data.Entities.Complex;

public class CumulativeAggregateMeasureEntity : AggregateMeasureEntity
{
}

public static class CumulativeAggregateMeasureEntityExtensions
{
  public static void CumulativeAggregateMeasure(
    this ComplexPropertyBuilder builder,
    string name,
    string unit
  )
  {
    builder.AggregateMeasure(name, unit);
  }
}
