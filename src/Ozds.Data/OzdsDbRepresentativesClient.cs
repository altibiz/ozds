using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities;
using Ozds.Data.Models;

namespace Ozds.Data;

public partial class OzdsDbClient
{
  public async Task CreateRepresentative(RepresentativeModel model)
  {
    await _context.Representatives.AddAsync(RepresentativeToEntity(model));
  }

  public async Task<RepresentativeModel?> ReadRepresentative(string id)
  {
    return await _context.Representatives.FirstOrDefaultAsync(entity =>
      entity.Id == id) is { } entity
      ? RepresentativeToModel(entity)
      : null;
  }

  public async Task UpdateRepresentative(RepresentativeModel model)
  {
    await _context.Representatives.SingleUpdateAsync(
      RepresentativeToEntity(model));
  }

  public async Task DeleteRepresentative(string id)
  {
    await _context.Representatives.DeleteByKeyAsync(id);
  }

  public async Task<RepresentativeModel?> ReadOperatorRepresentative()
  {
    return await _context.Representatives.FirstOrDefaultAsync(entity =>
      entity.IsOperatorRepresentative) is { } entity
      ? RepresentativeToModel(entity)
      : null;
  }

  public async Task<List<RepresentativeModel>> ReadNetworkUserRepresentatives(
    string id,
    int skip = 0,
    int take = TakeLimit
  )
  {
    return (await _context.Representatives
        .Where(entity => entity.NetworkUsers.Any(networkUser => networkUser.Id == id))
        .Skip(skip)
        .Take(take)
        .ToListAsync())
      .Select(RepresentativeToModel)
      .ToList();
  }

  public async Task<List<RepresentativeModel>> ReadLocationRepresentatives(
    string id,
    int skip = 0,
    int take = TakeLimit
  )
  {
    return (await _context.Representatives
        .Where(entity => entity.Locations.Any(networkUser => networkUser.Id == id))
        .Skip(skip)
        .Take(take)
        .ToListAsync())
      .Select(RepresentativeToModel)
      .ToList();
  }

  private static RepresentativeModel RepresentativeToModel(
    RepresentativeEntity entity)
  {
    return new RepresentativeModel(
      entity.Id,
      entity.UserId,
      entity.Name,
      entity.SocialSecurityNumber,
      entity.Address,
      entity.City,
      entity.PostalCode,
      entity.Email,
      entity.PhoneNumber
    );
  }

  private static RepresentativeEntity RepresentativeToEntity(
    RepresentativeModel model)
  {
    return new RepresentativeEntity
    {
      Id = model.Id,
      UserId = model.UserId,
      Name = model.Name,
      SocialSecurityNumber = model.SocialSecurityNumber,
      Address = model.Address,
      City = model.City,
      PostalCode = model.PostalCode,
      Email = model.Email,
      PhoneNumber = model.PhoneNumber
    };
  }
}
