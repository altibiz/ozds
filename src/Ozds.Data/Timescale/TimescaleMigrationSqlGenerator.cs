using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations;

// TODO: make number of partitions configurable
// NOTE: for now hardcored to 2 because we have two meter types

namespace Ozds.Data.Timescale;

public class TimescaleMigrationSqlGenerator : NpgsqlMigrationsSqlGenerator
{
  public TimescaleMigrationSqlGenerator(
    MigrationsSqlGeneratorDependencies dependencies,
#pragma warning disable EF1001
    INpgsqlSingletonOptions options
#pragma warning restore EF1001
  ) : base(dependencies, options)
  {
  }

  protected override void Generate(
    CreateTableOperation operation,
    IModel? model,
    MigrationCommandListBuilder builder,
    bool terminate = true
  )
  {
    base.Generate(
      operation,
      model,
      builder,
      terminate
    );

    if (operation.FindAnnotation("TimescaleHypertable")?.Value is string columnsString)
    {
      var columns = columnsString.Split(",");
      var timeColumn = columns.FirstOrDefault();
      if (timeColumn is null)
      {
        return;
      }
      var space = columns.LastOrDefault()?.Split(":");
      var spaceColumn = space?.FirstOrDefault();
      var spacePartitioning = space?.LastOrDefault();

      builder.AppendLine(
        $"SELECT create_hypertable('\"{operation.Name}\"', '{timeColumn}');"
      );
      if (spaceColumn is not null && spacePartitioning is not null)
      {
        builder.AppendLine(
          $"""
          SELECT add_dimension(
            '"{operation.Name}"',
            '{spaceColumn}',
            {spacePartitioning}
          );
          """
        );
      }

      if (terminate)
      {
        EndStatement(builder);
      }
    }
  }
}
