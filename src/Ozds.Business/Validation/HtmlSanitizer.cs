using System.ComponentModel.DataAnnotations;

namespace Ozds.Business.Validation;

public class HtmlSanitizer
{
  private readonly Ganss.Xss.HtmlSanitizer _sanitizer = new();

  public string Sanitize(string html)
  {
    return _sanitizer.Sanitize(html);
  }

  public ValidationResult? Validate(string html)
  {
    try
    {
      var sanitizedHtml = Sanitize(html);
      if (sanitizedHtml != html)
      {
        return new ValidationResult(
          "HTML contains invalid HTML",
          new[] { nameof(html) });
      }
    }
    catch (Exception ex)
    {
      return new ValidationResult(
        $"HTML contains invalid HTML:{Environment.NewLine}{ex.Message}",
        new[] { nameof(html) });
    }

    return default;
  }
}
