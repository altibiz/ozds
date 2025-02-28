using System.Security.Claims;
using Ozds.Users.Entities;

namespace Ozds.Users.Queries.Abstractions;

public interface IQueries
{
  Task<UserEntity?> ReadUserByClaimsPrincipal(
    ClaimsPrincipal principal,
    CancellationToken cancellationToken);

  Task<UserEntity?> ReadUserById(
    string id,
    CancellationToken cancellationToken);

  Task<(List<UserEntity> Items, int TotalCount)> ReadUsers(
    int pageNumber,
    int pageSize,
    CancellationToken cancellationToken);

  Task<string?> ReadAuthenticatedUserId(CancellationToken cancellationToken);
}
