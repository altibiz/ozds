using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class MeterQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public Task<List<(IAggregateEntity Month, IAggregateEntity MaxPower)>> ReadMonthlyMetrics
}
