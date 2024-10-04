using System.ComponentModel.DataAnnotations;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data.Context;

// TODO: check representative model user id

namespace Ozds.Business.Mutations.Agnostic;

public class OzdsAgnosticMutations(
  DataDbContext context,
  AgnosticModelEntityConverter modelEntityConverter
) : IMutations
{
  public Task Save()
  {
    return context.SaveChangesAsync();
  }

  public void Create(IAuditable model)
  {
    var validationResults = model
      .Validate(new ValidationContext(this))
      .ToList();
    if (validationResults.Count is not 0)
    {
      throw new ValidationException(validationResults.First().ErrorMessage);
    }

    context.Add(modelEntityConverter.ToEntity(model));
  }

  public void Update(IAuditable model)
  {
    var validationResults = model
      .Validate(new ValidationContext(this))
      .ToList();
    if (validationResults.Count is not 0)
    {
      throw new ValidationException(validationResults.First().ErrorMessage);
    }

    context.Update(modelEntityConverter.ToEntity(model));
  }

  public void Delete(IAuditable model)
  {
    context.Remove(modelEntityConverter.ToEntity(model));
  }
}
