using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ozds.Business.Interceptors;

public class ServedSaveChangesInterceptor(IServiceProvider serviceProvider)
  : SaveChangesInterceptor
{
  protected readonly IServiceProvider _serviceProvider = serviceProvider;
}
