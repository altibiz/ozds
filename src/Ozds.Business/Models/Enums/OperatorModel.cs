namespace Ozds.Business.Models.Enums;

public enum OperatorModel
{
  Sum,
  Average,
  Last
}

public static class OperatorModelExtensions
{
  public static string ToTitle(this OperatorModel operatorModel)
  {
    return operatorModel switch
    {
      OperatorModel.Sum => "sum",
      OperatorModel.Average => "average",
      OperatorModel.Last => "last",
      _ => throw new ArgumentOutOfRangeException(
        nameof(operatorModel),
        operatorModel,
        null)
    };
  }

  public static decimal Apply(
    this IEnumerable<decimal> values,
    OperatorModel operatorModel
  )
  {
    return operatorModel switch
    {
      OperatorModel.Sum => values.Sum(),
      OperatorModel.Average => values.Average(),
      OperatorModel.Last => values.LastOrDefault(),
      _ => throw new ArgumentOutOfRangeException(
        nameof(operatorModel),
        operatorModel,
        null)
    };
  }
}
