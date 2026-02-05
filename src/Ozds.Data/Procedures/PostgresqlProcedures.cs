using Microsoft.EntityFrameworkCore;
using Ozds.Data.Procedures.Abstractions;

// NOTE: \n is ok here because we're sending it to the database

namespace Ozds.Data.Procedures;

public class PostgresqlProcedures : IProcedures
{
  public string CallBatchMutation(
    DbContext context,
    Type type,
    string name,
    string jsonParameter
  )
  {
    var entityType =
      context.Model.FindEntityType(type)
      ?? throw new InvalidOperationException(
        $"No entity type found for {type}."
      );

    var tableName =
      entityType.GetTableName()
      ?? throw new InvalidOperationException(
        $"No table name found for {type}."
      );

    var tableFunctionName = tableName.EndsWith('s')
      ? tableName
      : tableName + "s";

    return $@"
      SELECT * FROM batch_{name}_{tableFunctionName}({jsonParameter});
    ";
  }

  public string CallMutation(
    DbContext context,
    Type type,
    string name,
    string jsonParameter
  )
  {
    var entityType =
      context.Model.FindEntityType(type)
      ?? throw new InvalidOperationException(
        $"No entity type found for {type}."
      );

    var tableName =
      entityType.GetTableName()
      ?? throw new InvalidOperationException(
        $"No table name found for {type}."
      );

    var tableFunctionName = tableName.EndsWith('s')
      ? tableName.TrimEnd('s')
      : tableName;

    return $@"
      SELECT * FROM {name}_{tableFunctionName}({jsonParameter});
    ";
  }

  public string UnionAll(IEnumerable<string> queries)
  {
    return string.Join("\nUNION ALL\n", queries);
  }

  public string OverwriteBatchMutation(
    DbContext context,
    string name,
    Type type,
    string query
  )
  {
    var entityType =
      context.Model.FindEntityType(type)
      ?? throw new InvalidOperationException(
        $"No entity type found for {type}."
      );

    var tableName =
      entityType.GetTableName()
      ?? throw new InvalidOperationException(
        $"No table name found for {type}."
      );

    var tableFunctionName = tableName.EndsWith('s')
      ? tableName
      : tableName + "s";

    return OverwriteFunction(
      $"batch_{name}_{tableFunctionName}",
      "entities jsonb",
      $"SETOF {tableName}",
      $@"
        RETURN QUERY
        {query}
      "
    );
  }

  public string OverwriteMutation(
    DbContext context,
    string name,
    Type type,
    string query
  )
  {
    var tableName =
      context.Model.FindEntityType(type)?.GetTableName()
      ?? throw new InvalidOperationException(
        $"No table name found for {type}."
      );

    var tableFunctionName = tableName.EndsWith('s')
      ? tableName.TrimEnd('s')
      : tableName;

    return OverwriteFunction(
      $"{name}_{tableFunctionName}",
      "entity jsonb",
      $"SETOF {tableName}",
      $@"
        RETURN QUERY
        {query}
      "
    );
  }

  public string OverwriteFunction(
    string name,
    string parameters,
    string @return,
    string body
  )
  {
    return $@"
      CREATE OR REPLACE FUNCTION {name}({parameters})
      RETURNS {@return}
      AS $$
      BEGIN
        {body}
      END;
      $$ LANGUAGE plpgsql;
    ";
  }

  public string DeleteBatchMutation(DbContext context, Type type, string name)
  {
    var entityType =
      context.Model.FindEntityType(type)
      ?? throw new InvalidOperationException(
        $"No entity type found for {type}."
      );

    var tableName =
      entityType.GetTableName()
      ?? throw new InvalidOperationException(
        $"No table name found for {type}."
      );

    var tableFunctionName = tableName.EndsWith('s')
      ? tableName
      : tableName + "s";

    return DeleteFunction(
      $"batch_{name}_{tableFunctionName}",
      "entities jsonb"
    );
  }

  public string DeleteMutation(DbContext context, Type type, string name)
  {
    var tableName =
      context.Model.FindEntityType(type)?.GetTableName()
      ?? throw new InvalidOperationException(
        $"No table name found for {type}."
      );

    var tableFunctionName = tableName.EndsWith('s')
      ? tableName.TrimEnd('s')
      : tableName;

    return DeleteFunction($"{name}_{tableFunctionName}", "entity jsonb");
  }

  public string DeleteFunction(string name, string parameters)
  {
    return $@"
      DROP FUNCTION IF EXISTS {name}({parameters});
    ";
  }
}
