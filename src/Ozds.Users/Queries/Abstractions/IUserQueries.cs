using System.Security.Claims;
using Ozds.Users.Models;

namespace Ozds.Users.Queries.Abstractions;

public interface IUserQueries
{
  Task<UserModel?> UserByClaimsPrincipal(ClaimsPrincipal principal);
  Task<UserModel?> UserByUserId(string userId);
  Task<(List<UserModel> Items, int TotalCount)> Users(int pageNumber, int pageSize);
}
