using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations;

namespace Ozds.Data.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class TimescaleHypertableAttribute : Attribute
{
}

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

    model?
      .GetEntityTypes()
       .SelectMany(entity => entity
         .GetProperties()
         .Select(property => new
         {
           Entity = entity,
           Property = property
         })
       )
       .Where(
         x => x.Property.PropertyInfo?.GetCustomAttribute<TimescaleHypertableAttribute>() is { }
       )
       .Aggregate(
          builder,
         (builder, x) => builder
          .AppendLine(
            $"""
              SELECT create_hypertable(
                '\"{x.Entity.GetTableName()}\"',
                '{x.Property.GetColumnName()}'
              );
            """
          ));
  }
}

