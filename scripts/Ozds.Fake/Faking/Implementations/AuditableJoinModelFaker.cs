using Ozds.Business.Models.Base;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations;

public class AuditableJoinModelFaker(IServiceProvider serviceProvider)
  : InheritingModelFaker<AuditableJoinModel, JoinModel>(serviceProvider) { }
