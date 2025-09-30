using Ozds.Business.Activation;
using Ozds.Business.Extensions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;
using Ozds.Time.Queries.Abstractions;

namespace Ozds.Business.Test.Activation;

public class ModelActivatorTest
{
  public static IEnumerable<Type> TestData()
  {
    return AppDomain.CurrentDomain
      .GetAssemblies()
      .Where(x => x.FullName is { } name && name.Contains("Ozds"))
      .SelectMany(
        assembly => assembly
          .GetTypes()
          .Where(
            type =>
              !type.IsGenericType
              && type.IsAssignableTo(typeof(IModel))));
  }

  // FIXME: nice way to register lots of services
  // [Test]
  [MethodDataSource(nameof(TestData))]
#pragma warning disable TUnit0019 // Missing `Test` Attribute
  public void Activates(Type modelType)
#pragma warning restore TUnit0019 // Missing `Test` Attribute
  {
    var builder = Host.CreateApplicationBuilder();
    builder.AddOzdsBusinessPure();
    builder.Services.AddScoped(
      _ => new Mock<TimeQueries>(
        MockBehavior.Loose,
        Mock.Of<ITimeQueries>()).Object);
    builder.Services.AddScoped(
      _ => new Mock<ClockQueries>(
        MockBehavior.Loose,
        Mock.Of<IClockQueries>()).Object);
    var host = builder.Build();

    var activator = host.Services
      .GetRequiredService<ModelActivator>();

    var model = activator.ActivateDynamic(modelType);
    model.Should().NotBeNull().And.BeAssignableTo(modelType);
  }
}
