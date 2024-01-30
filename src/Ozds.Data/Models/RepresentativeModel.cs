namespace Ozds.Data.Models;

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
