using Ozds.Business.Hosting;

namespace Ozds.Business.Test.Base;

public abstract class OzdsBusinessHostTestBase
{
  private static readonly Lazy<IHost> LazyHost = new(
    () =>
      new OzdsBusinessHost<HostApplicationBuilder, IHost>(
        Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder(),
        builder =>
        {
          builder.Configuration.AddInMemoryCollection(
            new Dictionary<string, string?>
            {
              { "Ozds:Business:Authorization:HmacSecret", "test" },
              {
                "Ozds:Data:ConnectionString",
                "Server=localhost;Port=5432;Database=ozds;User Id=ozds;Password=ozds"
              },
              { "Ozds:Data:SelfContainedReflection", "true" },
              { "Ozds:Caching:ConnectionString", "memory://" }
            }
          );
        },
        builder => { return builder.Build(); }
      ));

  public IHost Host
  {
    get { return LazyHost.Value; }
  }
}
