using Ozds.Users.Mutations.Abstractions;
using DataUserDeleteMutations = Ozds.Users.Mutations.DeleteMutations;

namespace Ozds.Business.Mutations;

public class UserDeleteMutations(DataUserDeleteMutations mutations)
  : IMutations
{
  public async Task DeleteUser(string id, CancellationToken cancellationToken)
  {
    await mutations.DeleteUser(id, cancellationToken);
  }
}
