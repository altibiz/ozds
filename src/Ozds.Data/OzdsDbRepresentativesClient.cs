using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities;
using Ozds.Data.Models;

namespace Ozds.Data;

public partial class OzdsDbClient
{
  public async Task CreateRepresentative(RepresentativeModel model) =>
    await context.Representatives.AddAsync(RepresentativeToEntity(model));

  public async Task<RepresentativeModel?> ReadRepresentative(string id) =>
    await context.Representatives.FirstOrDefaultAsync(entity => entity.Id == id) is { } entity
      ? RepresentativeToModel(entity)
      : null;

  public async Task UpdateRepresentative(RepresentativeModel model) =>
    await context.Representatives.SingleUpdateAsync(RepresentativeToEntity(model));

  public async Task DeleteRepresentative(string id) =>
    await context.Representatives.DeleteByKeyAsync(id);

  public async Task<RepresentativeModel?> ReadUserRepresentative(string id) =>
    await context.Representatives.FirstOrDefaultAsync(entity => entity.UserId == id) is { } entity
      ? RepresentativeToModel(entity)
      : null;

  public async Task<RepresentativeModel?> ReadOperatorRepresentative() =>
    await context.Representatives.FirstOrDefaultAsync(entity => entity.IsOperatorRepresentative) is { } entity
      ? RepresentativeToModel(entity)
      : null;

  public async Task<List<RepresentativeModel>> ReadTenantRepresentatives(string id) =>
    (await context.Representatives
      .Where(entity => entity.Tenants.Any(tenant => tenant.Id == id))
      .ToListAsync())
      .Select(RepresentativeToModel)
      .ToList();

  public async Task<List<RepresentativeModel>> ReadSubnetRepresentatives(string id) =>
    (await context.Representatives
      .Where(entity => entity.Subnets.Any(tenant => tenant.Id == id))
      .ToListAsync())
      .Select(RepresentativeToModel)
      .ToList();

  private static RepresentativeModel RepresentativeToModel(RepresentativeEntity entity) =>
    new(
      Id: entity.Id,
      UserId: entity.UserId,
      Name: entity.Name,
      SocialSecurityNumber: entity.SocialSecurityNumber,
      Address: entity.Address,
      City: entity.City,
      PostalCode: entity.PostalCode,
      Email: entity.Email,
      PhoneNumber: entity.PhoneNumber
    );

  private static RepresentativeEntity RepresentativeToEntity(RepresentativeModel model) =>
    new()
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
