using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Users.Entities;
using Ozds.Users.Mutations.Abstractions;
using UserMutationsBase = Ozds.Users.Mutations.UserMutations;

namespace Ozds.Business.Mutations;

public class UserMutations : IUserMutations
{
  private readonly ModelEntityConverter _converter;
  private readonly UserMutationsBase _mutations;

  public UserMutations(
    UserMutationsBase mutations,
    ModelEntityConverter converter
  )
  {
    _mutations = mutations;
    _converter = converter;
  }

  public async Task<UserModel> CreateUser(
    UserModel userModel,
    CancellationToken cancellationToken
  )
  {
    var userEntity = _converter.ToEntity<UserEntity>(userModel);
    var createdEntity = await _mutations.CreateUser(
      userEntity,
      cancellationToken
    );
    return _converter.ToModel<UserModel>(createdEntity);
  }

  public async Task UpdateUser(
    UserModel userModel,
    CancellationToken cancellationToken
  )
  {
    var userEntity = _converter.ToEntity<UserEntity>(userModel);
    await _mutations.UpdateUser(userEntity, cancellationToken);
  }

  public async Task DeleteUser(long id, CancellationToken cancellationToken)
  {
    await _mutations.DeleteUser(id, cancellationToken);
  }
}
