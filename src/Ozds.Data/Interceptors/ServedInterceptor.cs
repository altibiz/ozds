using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ozds.Data.Interceptors;

public abstract class ServedInterceptor(IServiceProvider serviceProvider)
  : SaveChangesInterceptor
{
#pragma warning disable SA1401
  protected readonly IServiceProvider serviceProvider = serviceProvider;

  public virtual int Order
  {
    get { return 100; }
  }
#pragma warning restore SA1401
}
