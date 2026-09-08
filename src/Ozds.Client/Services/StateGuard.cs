using System.Runtime.CompilerServices;

namespace Ozds.Client.Services;

public sealed class StateGuard : IDisposable
{
  private readonly Dictionary<CallSite, Dep[]> _entries = new();

  private bool _disposed;

  public bool Changed(
    Dep[] deps,
    [CallerFilePath] string? filePath = null,
    [CallerLineNumber] int lineNumber = 0
  )
  {
    ObjectDisposedException.ThrowIf(_disposed, this);

    var key = new CallSite(filePath, lineNumber);

    if (
      _entries.TryGetValue(key, out var prev)
      && prev.Length == deps.Length
      && DepsEqual(prev, deps)
    )
    {
      return false;
    }

    _entries[key] = Snapshot(deps);
    return true;
  }

  public void Dispose()
  {
    if (_disposed)
    {
      return;
    }

    _entries.Clear();
    _disposed = true;
  }

  private static bool DepsEqual(Dep[] prev, Dep[] next)
  {
    for (var i = 0; i < prev.Length; i++)
    {
      if (!next[i].Comparer(prev[i].Value, next[i].Value))
      {
        return false;
      }
    }

    return true;
  }

  private static Dep[] Snapshot(Dep[] deps)
  {
    var copy = new Dep[deps.Length];
    for (var i = 0; i < deps.Length; i++)
    {
      copy[i] = deps[i].WithSnapshottedValue();
    }

    return copy;
  }

  private readonly record struct CallSite(string? File, int Line);
}

public readonly struct Dep
{
  internal static readonly Func<object?, object?, bool> DefaultComparer =
    static (a, b) => Equals(a, b);

  internal readonly object? Value;

  private readonly Func<object?, object?, bool>? _comparer;
  private readonly Func<object?, object?>? _snapshot;

  private Dep(
    object? value,
    Func<object?, object?, bool>? comparer,
    Func<object?, object?>? snapshot
  )
  {
    Value = value;
    _comparer = comparer;
    _snapshot = snapshot;
  }

  internal Func<object?, object?, bool> Comparer =>
    _comparer ?? DefaultComparer;

  public static Dep Of<T>(T value) => new(value, null, null);

  public static Dep Of<T>(T value, Func<T?, T?, bool> equals) =>
    new(value, (a, b) => equals((T?)a, (T?)b), null);

  public static Dep Of<T>(T value, IEqualityComparer<T> comparer) =>
    new(value, (a, b) => comparer.Equals((T)a!, (T)b!), null);

  public static Dep SetEquality<T>(HashSet<T> value) =>
    new(
      value,
      static (a, b) =>
        (a is null) == (b is null)
        && (a is null || ((HashSet<T>)a).SetEquals((HashSet<T>)b!)),
      static v => new HashSet<T>((HashSet<T>)v!, ((HashSet<T>)v!).Comparer)
    );

  public static Dep SequenceEquality<T>(IEnumerable<T> value) =>
    new(
      value,
      static (a, b) =>
        (a is null) == (b is null)
        && (a is null || ((IEnumerable<T>)a).SequenceEqual((IEnumerable<T>)b!)),
      static v => ((IEnumerable<T>)v!).ToList()
    );

  internal Dep WithSnapshottedValue() =>
    _snapshot is null ? this : new Dep(_snapshot(Value), _comparer, null);
}
