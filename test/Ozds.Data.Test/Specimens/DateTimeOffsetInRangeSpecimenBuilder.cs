namespace Ozds.Data.Test.Specimens;

public class DateTimeOffsetInRangeSpecimenBuilder(
  DateTimeOffset fromDate,
  DateTimeOffset toDate
) : ISpecimenBuilder
{
  private static readonly Random _random = new();

  public object Create(object request, ISpecimenContext context)
  {
    if (request is Type t && t == typeof(DateTimeOffset))
    {
      var range = toDate - fromDate;
      var randomTicks = (long)(_random.NextDouble() * range.Ticks);
      return fromDate.AddTicks(randomTicks);
    }

    return new NoSpecimen();
  }
}
