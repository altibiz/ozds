using System.Data;
using System.Text.Json;
using Npgsql;
using NpgsqlTypes;
using static Dapper.SqlMapper;

namespace Ozds.Data.Context;

public class JsonParameter : ICustomQueryParameter
{
  private readonly JsonDocument? jsonDocument;
  private readonly JsonElement? jsonElement;

  public JsonParameter(JsonDocument json)
  {
    jsonDocument = json;
  }

  public JsonParameter(JsonElement json)
  {
    jsonElement = json;
  }

  public void AddParameter(IDbCommand command, string name)
  {
    if (jsonElement is not null)
    {
      command.Parameters.Add(
        new NpgsqlParameter(name, NpgsqlDbType.Jsonb) { Value = jsonElement }
      );
      return;
    }

    if (jsonDocument is not null)
    {
      command.Parameters.Add(
        new NpgsqlParameter(name, NpgsqlDbType.Jsonb) { Value = jsonDocument }
      );
      return;
    }

    throw new InvalidOperationException($"No json parameter found for {name}.");
  }
}
