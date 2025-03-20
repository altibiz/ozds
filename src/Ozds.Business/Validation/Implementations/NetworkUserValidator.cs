using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models;
using Ozds.Business.Validation.Base;

namespace Ozds.Business.Validation.Implementations;

public class NetworkUserValidator(
  HtmlSanitizer sanitizer
) : ConcreteModelValidator<NetworkUserModel>
{
  public override Task<List<ValidationResult>> ValidateAsync(
    NetworkUserModel model,
    CancellationToken cancellationToken
  )
  {
    var validationResults = new List<ValidationResult>();

    var validationResult = sanitizer.Validate(model.InvoiceRemark);
    if (validationResult is not null)
    {
      validationResults.Add(validationResult);
    }

    return Task.FromResult(validationResults);
  }
}
