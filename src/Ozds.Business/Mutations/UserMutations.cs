using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Users.Entities;
using UserUserMutations = Ozds.Users.Mutations.UserMutations;

namespace Ozds.Business.Mutations;

public class UserMutations(
  ModelEntityConverter converter,
  UserUserMutations mutations
) : IMutations
{
  public async Task<string> CreateUser(
    UserModel userModel,
    CancellationToken cancellationToken
  )
  {
    var userEntity = converter.ToEntity<UserEntity>(userModel);
    await mutations.CreateUser(
      userEntity,
      cancellationToken
    );
    return userEntity.Id;
  }

  public async Task UpdateUser(
    UserModel userModel,
    CancellationToken cancellationToken
  )
  {
    var userEntity = converter.ToEntity<UserEntity>(userModel);
    await mutations.UpdateUser(userEntity, cancellationToken);
  }

  public async Task DeleteUser(long id, CancellationToken cancellationToken)
  {
    await mutations.DeleteUser(id, cancellationToken);
  }
}
