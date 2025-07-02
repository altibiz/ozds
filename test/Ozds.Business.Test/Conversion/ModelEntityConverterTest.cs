using Ozds.Business.Activation;
using Ozds.Business.Conversion;
using Ozds.Business.Extensions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;
using Ozds.Time.Queries.Abstractions;

namespace Ozds.Business.Test.Conversion;

public class ModelEntityConverterTest
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
              !type.IsGenericType &&
              type.IsAssignableTo(typeof(IModel))));
  }

  [Test]
  [MethodDataSource(nameof(TestData))]
  public void Converts(Type modelType)
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

    using var scope = host.Services.CreateScope();
    var serviceProvider = scope.ServiceProvider;

    var activator = serviceProvider
      .GetRequiredService<ModelActivator>();
    var modelEntityConverter = serviceProvider
      .GetRequiredService<ModelEntityConverter>();

    var activationType = TestData()
      .FirstOrDefault(
        type =>
          !type.IsGenericType
          && type.IsAssignableTo(modelType))!;
    activationType.Should().NotBeNull();
    var activated = activator.ActivateDynamic(activationType);
    activated.Should().NotBeNull().And.BeAssignableTo(activationType);

    var entityType = modelEntityConverter.EntityType(activated.GetType());
    var entity = modelEntityConverter.ToEntity(activated);
    entity.Should().NotBeNull().And.BeAssignableTo(entityType);

    var converted = modelEntityConverter.ToModel(entity);
    converted.Should().NotBeNull().And.BeAssignableTo(modelType);
    converted.Should().BeEquivalentTo(activated);
  }
}
