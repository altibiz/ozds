using Ozds.Caching.Entities;
using Ozds.Caching.Entities.Composite;

namespace Ozds.Caching.Test.Specimens;

public class ApiKeyAuthEntityBuilder : ISpecimenBuilder
{
  public object Create(object request, ISpecimenContext context)
  {
    if (request is not Type t || t != typeof(ApiKeyAuthEntity))
    {
      return new NoSpecimen();
    }

    var apiKey = context.Create<ApiKeyEntity>();

    var scopes = new List<ScopeEntity>(3)
    {
      context.Create<MeasurementScopeEntity>(),
      context.Create<MeasurementScopeEntity>(),
      context.Create<ScopeEntity>(),
    };

    var registers = new List<RegisterEntity>(2);
    var register1 = context.Create<RegisterEntity>();
    register1.ScopeId = scopes[0].Id;
    registers.Add(register1);
    var register2 = context.Create<RegisterEntity>();
    register2.ScopeId = scopes[1].Id;
    registers.Add(register2);

    return new ApiKeyAuthEntity
    {
      ApiKey = apiKey,
      Scopes = scopes,
      Registers = registers,
    };
  }
}
