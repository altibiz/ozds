using System.Security.Claims;
using Ozds.Users.Entities;

namespace Ozds.Users.Queries.Abstractions;

public interface IUserQueries
{
  Task<UserEntity?> UserByClaimsPrincipal(ClaimsPrincipal principal, CancellationToken cancellationToken);

  Task<UserEntity?> UserById(string id, CancellationToken cancellationToken);

  Task<(List<UserEntity> Items, int TotalCount)> Users(
    int pageNumber,
    int pageSize,
    CancellationToken cancellationToken);
}
