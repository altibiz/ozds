using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using OrchardCore.Users;
using Ozds.Business.Models.Abstractions;
using Ozds.Data;
using ISession = YesSql.ISession;

// TODO: special checks for representative models

namespace Ozds.Business.Mutations;

public partial class OzdsAuditableMutations
{
  private readonly OzdsDbContext _context;

  private readonly ISession _session;

  private readonly UserManager<IUser> _userManager;

  public OzdsAuditableMutations(OzdsDbContext context,
    UserManager<IUser> userManager, ISession session)
  {
    _context = context;
    _session = session;
    _userManager = userManager;
  }

  public async Task SaveChanges()
  {
    await _context.SaveChangesAsync();
  }

  public List<ValidationResult>? Create(IAuditable auditable)
  {
    var validationResults = auditable
      .Validate(new ValidationContext(this))
      .ToList();
    if (validationResults.Count is not 0)
    {
      return validationResults;
    }

    _context.Add(EntityModelTypeMapper.ToEntity(auditable));
    return null;
  }

  public List<ValidationResult>? Update(IAuditable auditable)
  {
    var validationResults = auditable
      .Validate(new ValidationContext(this))
      .ToList();
    if (validationResults.Count is not 0)
    {
      return validationResults;
    }

    _context.Update(EntityModelTypeMapper.ToEntity(auditable));
    return null;
  }

  public void Delete(IAuditable auditable)
  {
    _context.Remove(EntityModelTypeMapper.ToEntity(auditable));
  }
}
