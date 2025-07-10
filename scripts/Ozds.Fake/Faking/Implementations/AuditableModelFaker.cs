using Ozds.Business.Models.Base;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations;

public class AuditableModelFaker(
  IServiceProvider serviceProvider
) : InheritingModelFaker<AuditableModel, IdentifiableModel>(serviceProvider)
{
}
