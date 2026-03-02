using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Users.Entities;
using UserPasswordMutations = Ozds.Users.Mutations.PasswordMutations;

namespace Ozds.Business.Mutations;

public class PasswordMutations(
  UserPasswordMutations userPasswordMutations,
  ModelUserEntityConverter modelEntityConverter
) : IMutations
{
  public async Task Update(
    PasswordModel model,
    CancellationToken cancellationToken
  )
  {
    var entity = modelEntityConverter.ToEntity<PasswordEntity>(model);
    await userPasswordMutations.Update(entity, cancellationToken);
  }

  public async Task Create(
    NewPasswordModel model,
    CancellationToken cancellationToken
  )
  {
    var entity = modelEntityConverter.ToEntity<NewPasswordEntity>(model);
    await userPasswordMutations.Create(entity, cancellationToken);
  }
}
