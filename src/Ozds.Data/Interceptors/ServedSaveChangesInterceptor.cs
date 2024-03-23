using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ozds.Data.Interceptors;

public class ServedSaveChangesInterceptor : SaveChangesInterceptor
{
  protected readonly IServiceProvider _serviceProvider;

  public ServedSaveChangesInterceptor(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }
}
