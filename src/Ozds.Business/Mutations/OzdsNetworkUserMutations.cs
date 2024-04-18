using System.ComponentModel.DataAnnotations;
using Ozds.Business.Conversion;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Extensions;
using Ozds.Business.Models;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data;

// TODO: check representative model user id

namespace Ozds.Business.Mutations.Agnostic;

public class OzdsNetworkUserMutations : IOzdsMutations
{
  private readonly OzdsDbContext _context;

  private readonly AgnosticModelEntityConverter _modelEntityConverter;

  public OzdsNetworkUserMutations(
    OzdsDbContext context,
    AgnosticModelEntityConverter modelEntityConverter
  )
  {
    _context = context;
    _modelEntityConverter = modelEntityConverter;
  }

  public async ValueTask DisposeAsync()
  {
    await _context.SaveChangesAsync();
    GC.SuppressFinalize(this);
  }

  public void Dispose()
  {
    _context.SaveChanges();
    GC.SuppressFinalize(this);
  }

  public void ClearChanges()
  {
    _context.ChangeTracker.Clear();
  }

  public void CreateWithRepresentatives(NetworkUserModel netUser,
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

  public void UpdateWithRepresentatives(NetworkUserModel netUser,
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
