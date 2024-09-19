using System.ComponentModel.DataAnnotations;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data.Context;
using INotification = Ozds.Business.Models.Abstractions.INotification;

namespace Ozds.Business.Mutations.Agnostic;

public class OzdsNotificationMutations(
  DataDbContext context,
  AgnosticModelEntityConverter modelEntityConverter
) : IMutations
{
  private readonly DataDbContext _context = context;

  private readonly AgnosticModelEntityConverter _modelEntityConverter =
    modelEntityConverter;

  public Task SaveChangesAsync()
  {
    return _context.SaveChangesAsync();
  }

  public void ClearChanges()
  {
    _context.ChangeTracker.Clear();
  }

  public void Create(INotification @event)
  {
    var validationResults = @event
      .Validate(new ValidationContext(this))
      .ToList();
    if (validationResults.Count is not 0)
    {
      throw new ValidationException(validationResults.First().ErrorMessage);
    }

    _context.Add(_modelEntityConverter.ToEntity(@event));
  }
}
