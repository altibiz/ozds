using Ozds.Business.Models.Base;
using Ozds.Client.Test.Faking.Base;

namespace Ozds.Client.Test.Faking.Implementations;

public class AuditableModelFaker(
  IServiceProvider serviceProvider
) : InheritingModelFaker<AuditableModel, IdentifiableModel>(serviceProvider)
{
}
