using Microsoft.EntityFrameworkCore.Diagnostics;

// TODO: implement automatic insertion of aggregations

namespace Ozds.Business.Interceptors;

public class AggregateCreationInterceptor : ServedSaveChangesInterceptor
{
  public AggregateCreationInterceptor(IServiceProvider serviceProvider)
    : base(serviceProvider) { }

  public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
  {
    CreateAggregates(eventData);
    return base.SavingChanges(eventData, result);
  }

  public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
  {
    CreateAggregates(eventData);
    return base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  private void CreateAggregates(DbContextEventData eventData)
  {
    throw new NotImplementedException();
  }
}
