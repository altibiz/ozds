using System.Security.Claims;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Entities;
using DataRepresentativeQueries = Ozds.Data.Queries.RepresentativeQueries;
using DataTrackableQueries = Ozds.Data.Queries.TrackableQueries;
using UserUserQueries = Ozds.Users.Queries.UserQueries;

namespace Ozds.Business.Queries;

public class RepresentativeQueries(
  DataRepresentativeQueries dataRepresentativeQueries,
  DataTrackableQueries dataTrackableQueries,
  ModelEntityConverter modelEntityConverter,
  ModelUserEntityConverter modelUserEntityConverter,
  UserUserQueries userUserQueries
) : IQueries
{
  public string LoginHref
  {
    get { return userUserQueries.LoginHref; }
  }

  public async Task<MaybeRepresentingUserModel?> ReadMaybeRepresentingUserByUserId(
    string id,
    CancellationToken cancellationToken
  )
  {
    var user = await userUserQueries.ReadUserById(id, cancellationToken);
    if (user is null)
    {
      return null;
    }

    var representative =
      await dataTrackableQueries.ReadById<RepresentativeEntity>(
        user.Id,
        cancellationToken
      );
    if (representative is null)
    {
      return new MaybeRepresentingUserModel
      {
        User = modelUserEntityConverter.ToModel<UserModel>(user),
        Representative = null,
      };
    }

    return new RepresentingUserModel
    {
      User = modelUserEntityConverter.ToModel<UserModel>(user),
      Representative = modelEntityConverter.ToModel<RepresentativeModel>(
        representative
      ),
    };
  }

  public async Task<
    PaginatedList<MaybeRepresentingUserModel>
  > ReadMaybeRepresentingUsers(
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount,
    bool deleted = false,
    string? search = null
  )
  {
    var users = await userUserQueries.ReadUsers(
      pageNumber,
      pageCount,
      cancellationToken,
      search
    );
    var userIds = users.Items.Select(user => user.Id).ToList();

    var representatives = await dataRepresentativeQueries.ReadByUserIds(
      userIds,
      cancellationToken,
      deleted
    );

    return users
      .Items.Select(user => new MaybeRepresentingUserModel
      {
        User = modelUserEntityConverter.ToModel<UserModel>(user),
        Representative = representatives.FirstOrDefault(x => x.Id == user.Id)
          is { } representative
          ? modelEntityConverter.ToModel<RepresentativeModel>(representative)
          : null,
      })
      .ToPaginatedList(users.TotalCount);
  }

  public async Task<MaybeRepresentingUserModel?> ReadMaybeRepresentingUserByClaimsPrincipal(
    ClaimsPrincipal claimsPrincipal,
    CancellationToken cancellationToken
  )
  {
    var user = await userUserQueries.ReadUserByClaimsPrincipal(
      claimsPrincipal,
      cancellationToken
    );
    if (user is null)
    {
      return null;
    }

    var representative =
      await dataTrackableQueries.ReadById<RepresentativeEntity>(
        user.Id,
        cancellationToken
      );
    if (representative is null)
    {
      return new MaybeRepresentingUserModel
      {
        User = modelUserEntityConverter.ToModel<UserModel>(user),
      };
    }

    return new RepresentingUserModel
    {
      User = modelUserEntityConverter.ToModel<UserModel>(user),
      Representative = modelEntityConverter.ToEntity<RepresentativeModel>(
        representative
      ),
    };
  }

  public async Task<UserModel?> ReadUserByClaimsPrincipal(
    ClaimsPrincipal claimsPrincipal,
    CancellationToken cancellationToken
  )
  {
    var user = await userUserQueries.ReadUserByClaimsPrincipal(
      claimsPrincipal,
      cancellationToken
    );
    return user is null
      ? null
      : modelUserEntityConverter.ToModel<UserModel>(user);
  }

  public async Task<UserModel?> ReadUserByUserId(
    string id,
    CancellationToken cancellationToken
  )
  {
    var user = await userUserQueries.ReadUserById(id, cancellationToken);
    return user is null
      ? null
      : modelUserEntityConverter.ToModel<UserModel>(user);
  }

  public async Task<RepresentativeModel?> ReadRepresentativeByUserId(
    string id,
    CancellationToken cancellationToken
  )
  {
    var representative =
      await dataTrackableQueries.ReadById<RepresentativeEntity>(
        id,
        cancellationToken
      );

    return representative is null
      ? null
      : modelEntityConverter.ToModel<RepresentativeModel>(representative);
  }

  public async Task<string?> ReadAuthenticatedRepresentativeId(
    CancellationToken cancellationToken
  )
  {
    var id = await userUserQueries.ReadAuthenticatedUserId(cancellationToken);

    return id;
  }
}
