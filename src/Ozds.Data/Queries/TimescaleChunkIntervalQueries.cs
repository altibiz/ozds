using Microsoft.EntityFrameworkCore;
using Ozds.Assets.Queries.Abstractions;
using Ozds.Data.Attributes;
using Ozds.Data.Context;
using Ozds.Data.Extensions;

namespace Ozds.Data.Queries;

// NOTE: this code is specific for measurement deletion job test
// hence it's not implemented further
[DapperResult]
public sealed class ChunkIntervalInfo
{
  public string HypertableName { get; init; } = "";

  public DateTimeOffset RangeStart { get; init; }

  public DateTimeOffset RangeEnd { get; init; }
}

public class TimescaleChunkIntervalQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<ChunkIntervalInfo?> GetChunkIntervalBeforeCutoff(
    DateTimeOffset threshold,
    Type entityType,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var tableName =
      context.GetTableName(entityType)
      ?? throw new InvalidOperationException(
        $"Table name not found for for {entityType.Name}."
      );

    const string sqlString = """
      SELECT @TableName            AS    "HypertableName",
             MIN(range_start)      AS    "RangeStart",
             MAX(range_end)        AS    "RangeEnd"
      FROM timescaledb_information.chunks
      WHERE hypertable_name = @TableName AND range_end < @Threshold;
      """;

    return (
      await context.DapperCommand<ChunkIntervalInfo>(
        sqlString,
        cancellationToken,
        new { TableName = tableName, Threshold = threshold }
      )
    ).FirstOrDefault();
  }

  public async Task<List<ChunkIntervalInfo>> GetChunkIntervalBeforeCutoff(
    DateTimeOffset threshold,
    IEnumerable<Type> entityTypes,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var hypertableNames = entityTypes
      .Select(x =>
        context.GetTableName(x)
        ?? throw new InvalidOperationException(
          $"Table name not found for for {x.Name}."
        )
      )
      .ToArray()!;

    const string sqlString = """
      SELECT hypertable_name      AS    "HypertableName",
             MIN(range_start)     AS    "RangeStart",
             MAX(range_end)       AS    "RangeEnd"
      FROM timescaledb_information.chunks
      WHERE range_end < @Threshold AND hypertable_name = ANY(@HypertableNames)
      GROUP BY hypertable_name;
      """;

    return await context.DapperCommand<ChunkIntervalInfo>(
      sqlString,
      cancellationToken,
      new { Threshold = threshold, HypertableNames = hypertableNames }
    );
  }
}
