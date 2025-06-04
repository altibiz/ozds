#pragma warning disable S3261
namespace Ozds.Server.Test;
#pragma warning restore S3261

// NOTE: for now because tests fail if a test project doesn't have
// at least one test

public class DummyTest
{
  [Test]
  public async Task DummyTestCase()
  {
#pragma warning disable TUnitAssertions0005 // Assert.That(...) should not be used with a constant value
    await Assert.That(true).IsTrue();
#pragma warning restore TUnitAssertions0005 // Assert.That(...) should not be used with a constant value
  }
}
