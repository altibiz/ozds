using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Ozds.Assets.Queries.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Extensions;
using Ozds.Data.Reflection;

namespace Ozds.Data.Queries;

public record ChunkInfo
{
  Type? HypertableName;

  DateTimeOffset? RangeStart;

  DateTimeOffset? RangeEnd;
}

public class TimescaleChunkIntervalQueries(
  IDbContextFactory<DataDbContext> factory,
  EntityReflector reflector,
  ILogger<TimescaleChunkIntervalQueries> logger
) : IQueries
{

  // this could be more specified to be for chunks of measurements
  public async Task<List<ChunkInfo>>
    GetChunkInformationForEntityTable(
      Type entityType,
      CancellationToken cancellationToken
    )
  {

    var context = await factory.CreateDbContextAsync(cancellationToken);

    var tableName = context.GetTableName(entityType);

    const string sqlString = """
      SELECT hypertable_name, range_start, range_end FROM timescaledb_information.chunks
      WHERE hypertable_name = @TableName
      ORDER BY range_start;
      """;

    var rows = await context.DapperCommand<ChunkInfo>(
        sqlString,
        cancellationToken,
        new { TableName = tableName }
      );

    return rows;
  }

  public async Task<ChunkInfo?>
   GetLatestChunkBeforeCutoff(
     DateTimeOffset threshold,
     Type entityType,
     CancellationToken cancellationToken
   )
  {

    var context = await factory.CreateDbContextAsync(cancellationToken);

    var tableName = context.GetTableName(entityType);

    const string sqlString = """
      SELECT hypertable_name, range_start, range_end FROM timescaledb_information.chunks
      WHERE hypertable_name = @TableName AND range_end < @Threshold
      ORDER BY range_end DESC
      LIMIT 1;
      """;

    var latestChunk = (await context.DapperCommand<ChunkInfo>(
        sqlString,
        cancellationToken,
        new {
          TableName = tableName,
          Threshold = threshold
        }
      )).FirstOrDefault();

    return latestChunk;
  }
}
