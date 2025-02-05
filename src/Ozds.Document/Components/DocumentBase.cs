using Microsoft.AspNetCore.Components;

namespace Ozds.Document.Components;

public abstract class DocumentBase : ComponentBase
{
  public string Translate(string notLocalized)
  {
    return notLocalized;
  }

  public string DateString(DateTimeOffset date)
  {
    return date.ToString("dd.MM.yyyy.");
  }

  public string NumericString(decimal number, int precision = 2)
  {
    return number.ToString($"0.{new string('#', precision)}");
  }

  public string NumericString(double number, int precision = 2)
  {
    return number.ToString($"0.{new string('#', precision)}");
  }
}
