using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ozds.Data.Extensions;

public static class MigrationBuilderExtensions
{
  public static void ConvertIntToEnum(
    this MigrationBuilder migrationBuilder,
    string tableName,
    string columnName,
    Dictionary<int, string> intToEnumMapping,
    string enumTypeName
  )
  {
    var sql = new StringBuilder();

    sql.AppendLine(
      $@"
                ALTER TABLE {
                  tableName
                }
                ADD COLUMN temp_{
                  columnName
                } {
                  enumTypeName
                };
            ");

    sql.AppendLine(
      $@"
                UPDATE {
                  tableName
                }
                SET temp_{
                  columnName
                } = CASE");

    foreach (var kvp in intToEnumMapping)
    {
      var enumIndex = kvp.Key;
      var enumName = kvp.Value;
      sql.AppendLine(
        $@"
                    WHEN {
                      columnName
                    } = {
                      enumIndex
                    } THEN '{
                      enumName
                    }'::{
                      enumTypeName
                    }");
    }

    sql.AppendLine(
      @"
                END;
            ");

    sql.AppendLine(
      $@"
                ALTER TABLE {
                  tableName
                }
                DROP COLUMN {
                  columnName
                };
            ");

    sql.AppendLine(
      $@"
                ALTER TABLE {
                  tableName
                }
                RENAME COLUMN temp_{
                  columnName
                } TO {
                  columnName
                };
            ");

    migrationBuilder.Sql(sql.ToString());
  }

  public static void ConvertEnumToInt(
    this MigrationBuilder migrationBuilder,
    string tableName,
    string columnName,
    Dictionary<string, int> enumToIntMapping
  )
  {
    var sql = new StringBuilder();

    sql.AppendLine(
      $@"
                ALTER TABLE {
                  tableName
                }
                ADD COLUMN temp_{
                  columnName
                } integer;
            ");

    sql.AppendLine(
      $@"
                UPDATE {
                  tableName
                }
                SET temp_{
                  columnName
                } = CASE");

    foreach (var kvp in enumToIntMapping)
    {
      var enumName = kvp.Key;
      var enumIndex = kvp.Value;
      sql.AppendLine(
        $@"
                    WHEN {
                      columnName
                    } = '{
                      enumName
                    }' THEN {
                      enumIndex
                    }");
    }

    sql.AppendLine(
      @"
                END;
            ");

    sql.AppendLine(
      $@"
                ALTER TABLE {
                  tableName
                }
                DROP COLUMN {
                  columnName
                };
            ");

    sql.AppendLine(
      $@"
                ALTER TABLE {
                  tableName
                }
                RENAME COLUMN temp_{
                  columnName
                } TO {
                  columnName
                };
            ");

    migrationBuilder.Sql(sql.ToString());
  }

  public static void ConvertIntArrayToEnumArray(
    this MigrationBuilder migrationBuilder,
    string tableName,
    string columnName,
    Dictionary<int, string> intToEnumMapping,
    string enumTypeName
  )
  {
    var sql = new StringBuilder();

    sql.AppendLine(
      $@"
                ALTER TABLE {
                  tableName
                }
                ADD COLUMN temp_{
                  columnName
                } {
                  enumTypeName
                }[];
            ");

    sql.AppendLine(
      $@"
                UPDATE {
                  tableName
                }
                SET temp_{
                  columnName
                } = ARRAY(
                    SELECT CASE");

    foreach (var kvp in intToEnumMapping)
    {
      var enumIndex = kvp.Key;
      var enumName = kvp.Value;
      sql.AppendLine(
        $@"
                    WHEN {
                      columnName
                    }_values = {
                      enumIndex
                    } THEN '{
                      enumName
                    }'::{
                      enumTypeName
                    }");
    }

    sql.AppendLine(
      $@"
                    END
                FROM unnest({
                  columnName
                }) AS {
                  columnName
                }_values
              );
            ");

    sql.AppendLine(
      $@"
                ALTER TABLE {
                  tableName
                }
                DROP COLUMN {
                  columnName
                };
            ");

    sql.AppendLine(
      $@"
                ALTER TABLE {
                  tableName
                }
                RENAME COLUMN temp_{
                  columnName
                } TO {
                  columnName
                };
            ");

    migrationBuilder.Sql(sql.ToString());
  }

  public static void ConvertEnumArrayToIntArray(
    this MigrationBuilder migrationBuilder,
    string tableName,
    string columnName,
    Dictionary<string, int> enumToIntMapping
  )
  {
    var sql = new StringBuilder();

    sql.AppendLine(
      $@"
                ALTER TABLE {
                  tableName
                }
                ADD COLUMN temp_{
                  columnName
                } integer[];
            ");

    sql.AppendLine(
      $@"
                UPDATE {
                  tableName
                }
                SET temp_{
                  columnName
                } = ARRAY(
                    SELECT CASE");

    foreach (var kvp in enumToIntMapping)
    {
      var enumName = kvp.Key;
      var enumIndex = kvp.Value;
      sql.AppendLine(
        $@"
                    WHEN {
                      columnName
                    }_values = '{
                      enumName
                    }' THEN {
                      enumIndex
                    }");
    }

    sql.AppendLine(
      $@"
                    END
                FROM unnest({
                  columnName
                }) AS {
                  columnName
                }_values
              );
            ");

    sql.AppendLine(
      $@"
                ALTER TABLE {
                  tableName
                }
                DROP COLUMN {
                  columnName
                };
            ");

    sql.AppendLine(
      $@"
                ALTER TABLE {
                  tableName
                }
                RENAME COLUMN temp_{
                  columnName
                } TO {
                  columnName
                };
            ");

    migrationBuilder.Sql(sql.ToString());
  }
}
