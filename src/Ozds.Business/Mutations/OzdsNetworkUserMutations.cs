using System.ComponentModel.DataAnnotations;
using Ozds.Business.Conversion;
using Ozds.Business.Extensions;
using Ozds.Business.Models;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data;

// TODO: check representative model user id

namespace Ozds.Business.Mutations.Agnostic;

public class OzdsNetworkUserMutations(
  OzdsDataDbContext context
) : IOzdsMutations
{
  private readonly OzdsDataDbContext _context = context;

  public Task SaveChangesAsync()
  {
    return _context.SaveChangesAsync();
  }

  public void ClearChanges()
  {
    _context.ChangeTracker.Clear();
  }

  public void CreateWithRepresentatives(
    NetworkUserModel netUser,
    IEnumerable<RepresentativeModel> reps)
  {
    var validationResults = netUser
      .Validate(new ValidationContext(this))
      .ToList();
    if (validationResults.Count is not 0)
    {
      throw new ValidationException(validationResults.First().ErrorMessage);
    }

    var repEntities = reps.Select(x => x.ToEntity()).ToList();
    var entity = netUser.ToEntity();
    _context.AddTracked(entity);
    _context.JoinTracked(entity, repEntities);
  }

  public void UpdateWithRepresentatives(
    NetworkUserModel netUser,
    IEnumerable<RepresentativeModel> reps)
  {
    var validationResults = netUser
      .Validate(new ValidationContext(this))
      .ToList();
    if (validationResults.Count is not 0)
    {
      throw new ValidationException(validationResults.First().ErrorMessage);
    }

    var repEntities = reps.Select(x => x.ToEntity()).ToList();
    var entity = netUser.ToEntity();
    entity.Representatives = repEntities;
    _context.UpdateTracked(entity);
    _context.JoinTracked(entity, repEntities);
  }
}
