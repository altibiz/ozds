using OrchardCore.Users.Models;
using Ozds.Users.Mutations.Abstractions;
using DataUserCreateMutations = Ozds.Users.Mutations.CreateMutations;

namespace Ozds.Business.Mutations
{
  public class UserCreateMutations(DataUserCreateMutations mutations)
    : IMutations
  {
    public async Task<User> CreateUser(
      string userName,
      string userEmail,
      CancellationToken cancellationToken
    )
    {
      return await mutations.CreateUser(userName, userEmail, cancellationToken);
    }
  }
}
