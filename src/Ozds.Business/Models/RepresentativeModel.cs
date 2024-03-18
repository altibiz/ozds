using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record RepresentativeModel(
  string Id,
  string UserId,
  string Name,
  string SocialSecurityNumber,
  string Address,
  string City,
  string PostalCode,
  string Email,
  string PhoneNumber
);

public static class RepresentativeModelExtensions
{
  public static RepresentativeEntity ToEntity(this RepresentativeModel model) =>
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

  public static RepresentativeModel ToModel(this RepresentativeEntity model) =>
    new(
      model.Id,
      model.UserId,
      model.Name,
      model.SocialSecurityNumber,
      model.Address,
      model.City,
      model.PostalCode,
      model.Email,
      model.PhoneNumber
    );
}
