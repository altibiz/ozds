namespace Ozds.Server.Test.Fixtures;

public class DebugFixture
{
  public async Task WaitIndefinitely(CancellationToken cancellationToken)
  {
    await Task.Delay(-1, cancellationToken);
  }
}
