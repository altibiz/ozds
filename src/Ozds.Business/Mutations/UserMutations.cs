using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Users.Entities;
using UserUserMutations = Ozds.Users.Mutations.UserMutations;

namespace Ozds.Business.Mutations;

public class UserMutations(
  ModelUserEntityConverter converter,
  UserUserMutations mutations
) : IMutations
{
  public async Task<string> Create(
    UserModel model,
    CancellationToken cancellationToken
  )
  {
    var entity = converter.ToEntity<UserEntity>(model);
    await mutations.Create(
      entity,
      cancellationToken
    );
    return entity.Id;
  }

  public async Task Update(
    UserModel userModel,
    CancellationToken cancellationToken
  )
  {
    var entity = converter.ToEntity<UserEntity>(userModel);
    await mutations.Update(entity, cancellationToken);
  }

  public async Task Delete(string id, CancellationToken cancellationToken)
  {
    await mutations.Delete(id, cancellationToken);
  }
}
