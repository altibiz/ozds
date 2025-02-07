using Ozds.Users.Mutations.Abstractions;
using DataUserUpdateMutations = Ozds.Users.Mutations.UpdateMutations;

namespace Ozds.Business.Mutations;

public class UserUpdateMutations(DataUserUpdateMutations mutations)
  : IMutations
{
  public async Task UpdateUser(
    string id,
    string userName,
    string userEmail,
    CancellationToken cancellationToken
  )
  {
    await mutations.UpdateUser(id, userName, userEmail, cancellationToken);
  }
}
