using Ozds.Business.Models.Base;
using Ozds.Business.Models.Joins;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Implementations.Administration;

public class ApiKeyScopeModelFaker(IServiceProvider serviceProvider)
  : InheritingModelFaker<ApiKeyScopeModel, AuditableJoinModel>(
    serviceProvider
  ) { }
