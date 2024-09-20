using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data.Context;

namespace Ozds.Business.Mutations.Agnostic;

public class OzdsAuditableMutations(
  IDbContextFactory<DataDbContext> factory,
  AgnosticModelEntityConverter modelEntityConverter
) : IMutations
{
  public async Task Create(IAuditable auditable)
  {
    var validationResults = auditable
      .Validate(new ValidationContext(this))
      .ToList();
    if (validationResults.Count is not 0)
    {
      throw new ValidationException(validationResults.First().ErrorMessage);
    }

    await using var context = await factory.CreateDbContextAsync();
    context.Add(modelEntityConverter.ToEntity(auditable));
    await context.SaveChangesAsync();
  }

  public async Task Update(IAuditable auditable)
  {
    var validationResults = auditable
      .Validate(new ValidationContext(this))
      .ToList();
    if (validationResults.Count is not 0)
    {
      throw new ValidationException(validationResults.First().ErrorMessage);
    }

    await using var context = await factory.CreateDbContextAsync();
    context.Update(modelEntityConverter.ToEntity(auditable));
    await context.SaveChangesAsync();
  }

  public async Task Delete(IAuditable auditable)
  {
    await using var context = await factory.CreateDbContextAsync();
    context.Remove(modelEntityConverter.ToEntity(auditable));
    await context.SaveChangesAsync();
  }
}
