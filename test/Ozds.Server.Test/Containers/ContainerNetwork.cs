using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Networks;
using Ozds.Server.Test.Extensions;

namespace Ozds.Server.Test.Containers;

public sealed class ContainerNetwork : IComposableService<ContainerNetwork>
{
  private static readonly HashSet<int> TakenPorts = new();

#pragma warning disable S4487 // Unread "private" fields should be removed
  private readonly string bridgeName;
#pragma warning restore S4487 // Unread "private" fields should be removed

  private readonly ConcurrentDictionary<string, string> hosts = new();

  private readonly INetwork network;

  private readonly ConcurrentDictionary<string, int> ports = new();

  private ContainerNetwork(
    INetwork network,
    string name,
    string bridgeName
  )
  {
    this.network = network;
    Name = name;
    this.bridgeName = bridgeName;
  }

  public string Name { get; }

  public static Task<ContainerNetwork> Create(
    ContainerNetwork network,
    CancellationToken cancellationToken
  )
  {
    var name = $"ozds-client-test-network-{Guid.NewGuid()}";
    var bridgeName = $"ozds-client-test-bridge-{Guid.NewGuid()}";

    var actualNetwork = new NetworkBuilder()
      .WithName(name)
      .WithDriver(NetworkDriver.Bridge)
      .WithOption("com.network.bridge.name", name)
      .Build();

    return Task.FromResult(
      new ContainerNetwork(actualNetwork, name, bridgeName));
  }

  public Task Configure(
    ServiceComposition composition,
    CancellationToken cancellationToken
  )
  {
    return Task.CompletedTask;
  }

  public async Task Start(CancellationToken cancellationToken)
  {
    await network.CreateAsync(cancellationToken);
  }

  public async Task Stop(CancellationToken cancellationToken)
  {
    await network.DeleteAsync(cancellationToken);
  }

  public async ValueTask DisposeAsync()
  {
    await network.DisposeAsync();
  }

  public string Host<T>()
  {
    var typeName = typeof(T).FullName
      ?? throw new InvalidOperationException(
        "Type must have a FullName");

    return hosts.GetOrAdd(
      typeName,
      typeName => MakeHostName(typeof(T))
    );
  }

  public int Port<T>()
  {
    var typeName = typeof(T).FullName
      ?? throw new InvalidOperationException(
        "Type must have a FullName");

    return ports.GetOrAdd(
      typeName,
      _ => FindFreePort()
    );
  }

  public int Port<T>(string name)
  {
    var typeName = typeof(T).FullName
      ?? throw new InvalidOperationException(
        "Type must have a FullName");

    return ports.GetOrAdd(
      $"{typeName}-{name}",
      _ => FindFreePort()
    );
  }

  private static string MakeHostName(Type type)
  {
    var typeName = type.Name;
    var normalized = typeName.NormalizeForHostName();
    var initial = $"ozds-client-test-{normalized}-{Guid.NewGuid()}";
    return initial[..Math.Min(initial.Length, 63)];
  }

  private static int FindFreePort()
  {
    static int FindFreePort()
    {
      var port = 0;
      var socket = new Socket(
        AddressFamily.InterNetwork,
        SocketType.Stream,
        ProtocolType.Tcp
      );
      try
      {
        var localEP = new IPEndPoint(IPAddress.Any, 0);
        socket.Bind(localEP);
        localEP = socket.LocalEndPoint as IPEndPoint
          ?? throw new InvalidOperationException(
            "Could not bind to local endpoint");
        port = localEP.Port;
      }
      finally
      {
        socket.Close();
      }

      return port;
    }

    var port = FindFreePort();
    lock (TakenPorts)
    {
      while (TakenPorts.Contains(port))
      {
        port = FindFreePort();
      }

      TakenPorts.Add(port);
    }

    return port;
  }
}
