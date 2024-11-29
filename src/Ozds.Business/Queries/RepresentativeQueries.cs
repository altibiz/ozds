using System.Security.Claims;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Entities;
using Ozds.Users.Queries.Abstractions;
using DataAuditableQueries = Ozds.Data.Queries.Agnostic.AuditableQueries;
using DataRepresentativeQueries = Ozds.Data.Queries.RepresentativeQueries;

namespace Ozds.Business.Queries;

public class RepresentativeQueries(
  DataRepresentativeQueries dataRepresentativeQueries,
  DataAuditableQueries dataAuditableQueries,
  IUserQueries userQueries
) : IQueries
{
  public async Task<MaybeRepresentingUserModel?>
    MaybeRepresentingUserByUserId(
      string id,
      CancellationToken cancellationToken
    )
  {
    var user = await userQueries.UserById(id, cancellationToken);
    if (user is null)
    {
      return null;
    }

    var representative = await dataAuditableQueries
      .ReadSingle<RepresentativeEntity>(user.Id, cancellationToken);
    if (representative is null)
    {
      return new MaybeRepresentingUserModel(user.ToModel(), null);
    }

    return new RepresentingUserModel(
      user.ToModel(),
      representative.ToModel()
    );
  }

  public async Task<PaginatedList<MaybeRepresentingUserModel>>
    MaybeRepresentingUsers(
      int pageNumber,
      CancellationToken cancellationToken,
      int pageCount = QueryConstants.DefaultPageCount,
      bool deleted = false
    )
  {
    var users = await userQueries.Users(pageNumber, pageCount, cancellationToken);
    var userIds = users.Items
      .Select(user => user.Id)
      .ToList();

    var representatives = await dataRepresentativeQueries.ReadByUserIds(
      userIds,
      cancellationToken,
      deleted
    );

    return users.Items
      .Select(
        user => new MaybeRepresentingUserModel(
          user.ToModel(),
          representatives
            .FirstOrDefault(x => x.Id == user.Id)
              is { } representative
              ? representative.ToModel()
              : null
        ))
      .ToPaginatedList(users.TotalCount);
  }

  public async Task<MaybeRepresentingUserModel?>
    MaybeRepresentingUserByClaimsPrincipal(
      ClaimsPrincipal claimsPrincipal,
      CancellationToken cancellationToken)
  {
    var user = await userQueries
      .UserByClaimsPrincipal(claimsPrincipal, cancellationToken);
    if (user is null)
    {
      return null;
    }

    var representative = await dataAuditableQueries
      .ReadSingle<RepresentativeEntity>(user.Id, cancellationToken);
    if (representative is null)
    {
      return new MaybeRepresentingUserModel(user.ToModel(), null);
    }

    return new RepresentingUserModel(
      user.ToModel(),
      representative.ToModel()
    );
  }

  public async Task<UserModel?> UserByClaimsPrincipal(
    ClaimsPrincipal claimsPrincipal,
    CancellationToken cancellationToken
  )
  {
    var user = await userQueries.UserByClaimsPrincipal(
      claimsPrincipal,
      cancellationToken
    );
    return user?.ToModel();
  }

  public async Task<RepresentativeModel?> RepresentativeByUserId(
    string id,
    CancellationToken cancellationToken
  )
  {
    var representative = await dataAuditableQueries
      .ReadSingle<RepresentativeEntity>(id, cancellationToken);
    if (representative is null)
    {
      return null;
    }

    return representative.ToModel();
  }
}
