using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ozds.Data.Interceptors;

public class PublishingInterceptor(IServiceProvider serviceProvider)
  : ServedSaveChangesInterceptor(serviceProvider)
{
  public override ValueTask<int> SavedChangesAsync(
    SaveChangesCompletedEventData eventData,
    int result,
    CancellationToken cancellationToken = default)
  {
    return base.SavedChangesAsync(eventData, result, cancellationToken);
  }
}
