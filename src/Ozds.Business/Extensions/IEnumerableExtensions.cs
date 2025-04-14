using System.Runtime.CompilerServices;

namespace Ozds.Business.Extensions;

public static class IEnumerableExtensions
{
  public static async IAsyncEnumerable<List<T>> Chunk<T>(
    this IAsyncEnumerable<T> enumerable,
    [EnumeratorCancellation] CancellationToken cancellationToken,
    int chunkSize = 1000
  )
  {
    var chunk = new List<T>();
    await foreach (var item in enumerable)
    {
      if (cancellationToken.IsCancellationRequested)
      {
        yield break;
      }

      chunk.Add(item);

      if (chunk.Count >= chunkSize)
      {
        yield return chunk;
        chunk = new List<T>();
      }
    }

    yield return chunk;
  }

  public static IEnumerable<dynamic> AsDynamic(
    this IEnumerable<object> enumerable
  )
  {
    return enumerable.Select(x => (dynamic)x);
  }

  public static IAsyncEnumerable<dynamic> AsDynamic(
    this IAsyncEnumerable<object> enumerable
  )
  {
    return enumerable.Select(x => (dynamic)x);
  }
}
