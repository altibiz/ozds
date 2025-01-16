using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation;

public class NetworkUserCatalogueModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
  NetworkUserCatalogueModel,
  AuditableModel>(serviceProvider)
{
}
