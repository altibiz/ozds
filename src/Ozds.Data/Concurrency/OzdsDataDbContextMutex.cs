namespace Ozds.Data.Concurrency;

public class OzdsDataDbContextMutex(OzdsDataDbContext context)
{
  private readonly SemaphoreSlim semaphore = new(1, 1);

  private readonly OzdsDataDbContext _context = context;

  public OzdsDataDbContextLock Lock()
  {
    semaphore.Wait();
    return new OzdsDataDbContextLock(this);
  }

  public async ValueTask<OzdsDataDbContextLock> LockAsync()
  {
    await semaphore.WaitAsync();
    return new OzdsDataDbContextLock(this);
  }

#pragma warning disable S3881 // "IDisposable" should be implemented correctly
  public class OzdsDataDbContextLock(OzdsDataDbContextMutex mutex) : IDisposable
#pragma warning restore S3881 // "IDisposable" should be implemented correctly
  {
    public OzdsDataDbContext Context => mutex._context;

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
    public void Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
    {
      mutex.semaphore.Release();
    }
  }
}
