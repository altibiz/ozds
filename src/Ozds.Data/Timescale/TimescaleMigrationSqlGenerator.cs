using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations;

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
      var spaceColumns = columns.Skip(1);

      builder.AppendLine(
        $"SELECT create_hypertable('\"{operation.Name}\"', '{timeColumn}');"
      );
      foreach (var spaceColumn in spaceColumns)
      {
        builder.AppendLine(
          $"SELECT add_dimension('\"{operation.Name}\"', '{spaceColumn}');"
        );
      }

      if (terminate)
      {
        builder.AppendLine(";");
        EndStatement(builder);
      }
    }
  }
}
