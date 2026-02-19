using System.Text.Json.Nodes;

namespace Ozds.Caching.Extensions;

public static class JsonExtensions
{
  public static JsonNode Merge(this JsonNode jsonBase, JsonNode jsonMerge)
  {
    switch (jsonBase)
    {
      case JsonObject jsonBaseObj when jsonMerge is JsonObject jsonMergeObj:
      {
        var mergeNodesArray = jsonMergeObj.ToArray();
        jsonMergeObj.Clear();

        foreach (var prop in mergeNodesArray)
        {
          jsonBaseObj[prop.Key] = jsonBaseObj[prop.Key] switch
          {
            JsonObject jsonBaseChildObj
              when prop.Value is JsonObject jsonMergeChildObj =>
              jsonBaseChildObj.Merge(jsonMergeChildObj),
            JsonArray jsonBaseChildArray
              when prop.Value is JsonArray jsonMergeChildArray =>
              jsonBaseChildArray.Merge(jsonMergeChildArray),
            _ => prop.Value,
          };
        }

        break;
      }
      case JsonArray jsonBaseArray when jsonMerge is JsonArray jsonMergeArray:
      {
        var mergeNodesArray = jsonMergeArray.ToArray();
        jsonMergeArray.Clear();
        foreach (var mergeNode in mergeNodesArray)
        {
          jsonBaseArray.Add(mergeNode);
        }

        break;
      }
      default:
        throw new ArgumentException(
          $"'{jsonBase.GetType().Name}'"
            + $" is incompatible with '{jsonMerge.GetType().Name}'"
        );
    }

    return jsonBase;
  }
}
