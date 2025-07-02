namespace Ozds.Client.Test.Containers;

public sealed class ServiceComposition : IAsyncDisposable
{
  private ServiceComposition(
    ContainerNetwork network,
    PostgresContainer postgres,
    RabbitMqContainer rabbitMq,
    MailpitContainer mailpit,
    AutheliaContainer authelia,
    LldapContainer lldap,
    AltibizFake altibiz,
    OzdsServer ozds,
    PlaywrightBrowser playwright
  )
  {
    Network = network;
    Postgres = postgres;
    RabbitMq = rabbitMq;
    Mailpit = mailpit;
    Authelia = authelia;
    Lldap = lldap;
    Altibiz = altibiz;
    Ozds = ozds;
    Playwright = playwright;
  }

  public ContainerNetwork Network { get; private set; }

  public PostgresContainer Postgres { get; private set; }

  public RabbitMqContainer RabbitMq { get; private set; }

  public MailpitContainer Mailpit { get; private set; }

  public AutheliaContainer Authelia { get; private set; }

  public LldapContainer Lldap { get; private set; }

  public AltibizFake Altibiz { get; private set; }

  public OzdsServer Ozds { get; private set; }

  public PlaywrightBrowser Playwright { get; private set; }

  public async ValueTask DisposeAsync()
  {
    await Playwright.DisposeAsync();
    Playwright = default!;
    await Ozds.DisposeAsync();
    Ozds = default!;
    await Altibiz.DisposeAsync();
    Altibiz = default!;
    await Authelia.DisposeAsync();
    Authelia = default!;
    await Lldap.DisposeAsync();
    Lldap = default!;
    await Mailpit.DisposeAsync();
    Mailpit = default!;
    await RabbitMq.DisposeAsync();
    RabbitMq = default!;
    await Postgres.DisposeAsync();
    Postgres = default!;
    await Network.DisposeAsync();
    Network = default!;
  }

  public static async Task<ServiceComposition> Create(
    CancellationToken cancellationToken
  )
  {
    var network = await ContainerNetwork.Create(null!, cancellationToken);
    await network.Configure(null!, cancellationToken);
    await network.Start(cancellationToken);

    var postgres = await PostgresContainer.Create(
      network,
      cancellationToken
    );
    var rabbitMq = await RabbitMqContainer.Create(
      network,
      cancellationToken
    );
    var mailpit = await MailpitContainer.Create(
      network,
      cancellationToken
    );
    var lldap = await LldapContainer.Create(
      network,
      cancellationToken
    );
    var authelia = await AutheliaContainer.Create(
      network,
      cancellationToken
    );
    var altibiz = await AltibizFake.Create(
      network,
      cancellationToken
    );
    var ozds = await OzdsServer.Create(
      network,
      cancellationToken
    );
    var playwright = await PlaywrightBrowser.Create(
      network,
      cancellationToken
    );

    var composition = new ServiceComposition(
      network,
      postgres,
      rabbitMq,
      mailpit,
      authelia,
      lldap,
      altibiz,
      ozds,
      playwright
    );

    await composition.Postgres.Configure(composition, cancellationToken);
    await composition.RabbitMq.Configure(composition, cancellationToken);
    await composition.Mailpit.Configure(composition, cancellationToken);
    await composition.Lldap.Configure(composition, cancellationToken);
    await composition.Authelia.Configure(composition, cancellationToken);
    await composition.Altibiz.Configure(composition, cancellationToken);
    await composition.Ozds.Configure(composition, cancellationToken);
    await composition.Playwright.Configure(composition, cancellationToken);

    return composition;
  }

  public async Task Start(CancellationToken cancellationToken)
  {
    await Postgres.Start(cancellationToken);
    await RabbitMq.Start(cancellationToken);
    await Mailpit.Start(cancellationToken);
    await Lldap.Start(cancellationToken);
    await Authelia.Start(cancellationToken);
    await Altibiz.Start(cancellationToken);
    await Ozds.Start(cancellationToken);
    await Playwright.Start(cancellationToken);
  }

  public async Task Stop(CancellationToken cancellationToken)
  {
    await Playwright.Stop(cancellationToken);
    await Ozds.Stop(cancellationToken);
    await Altibiz.Stop(cancellationToken);
    await Authelia.Stop(cancellationToken);
    await Lldap.Stop(cancellationToken);
    await Mailpit.Stop(cancellationToken);
    await RabbitMq.Stop(cancellationToken);
    await Postgres.Stop(cancellationToken);
    await Network.Stop(cancellationToken);
  }
}
