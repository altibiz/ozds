using Ozds.Business.Conversion;
using Ozds.Business.Models.Composite;
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
    UserWithPasswordModel model,
    CancellationToken cancellationToken
  )
  {
    var entity = converter.ToEntity<UserWithPasswordEntity>(model);
    await mutations.CreateUser(
      entity,
      cancellationToken
    );
    return entity.User.Id;
  }

  public async Task UpdateUser(
    UserWithPasswordModel userModel,
    CancellationToken cancellationToken
  )
  {
    var userEntity = converter.ToEntity<UserWithPasswordEntity>(userModel);
    await mutations.UpdateUser(userEntity, cancellationToken);
  }

  public async Task DeleteUser(string id, CancellationToken cancellationToken)
  {
    await mutations.DeleteUser(id, cancellationToken);
  }
}
