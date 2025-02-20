namespace Ozds.Client.Extensions;

public static class IAsyncEnumerableExtensions
{
#pragma warning disable CS8425 // Async-iterator member has one or more parameters of type 'CancellationToken' but none of them is decorated with the 'EnumeratorCancellation' attribute, so the cancellation token parameter from the generated 'IAsyncEnumerable<>.GetAsyncEnumerator' will be unconsumed
  public static async IAsyncEnumerable<List<T>> Chunk<T>(
    this IAsyncEnumerable<T> enumerable,
    CancellationToken cancellationToken,
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
#pragma warning restore CS8425 // Async-iterator member has one or more parameters of type 'CancellationToken' but none of them is decorated with the 'EnumeratorCancellation' attribute, so the cancellation token parameter from the generated 'IAsyncEnumerable<>.GetAsyncEnumerator' will be unconsumed
}
