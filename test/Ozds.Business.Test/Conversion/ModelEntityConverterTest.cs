using Ozds.Business.Activation;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Test.Base;

namespace Ozds.Business.Test.Conversion;

public class ModelEntityConverterTest : OzdsBusinessHostTestBase
{
  public static IEnumerable<Type> TestData()
  {
    return AppDomain.CurrentDomain
      .GetAssemblies()
      .Where(x => x.FullName is { } name && name.Contains("Ozds"))
      .SelectMany(assembly => assembly
        .GetTypes()
        .Where(type =>
          !type.IsGenericType &&
          type.IsAssignableTo(typeof(IModel))));
  }

  [Test]
  [MethodDataSource(nameof(TestData))]
  public void Converts(Type modelType)
  {
    using var scope = Host.Services.CreateScope();
    var serviceProvider = scope.ServiceProvider;

    var activator = serviceProvider
      .GetRequiredService<ModelActivator>();
    var modelEntityConverter = serviceProvider
      .GetRequiredService<ModelEntityConverter>();

    var activationType = TestData()
      .FirstOrDefault(type =>
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
    converted.Should().BeEquivalentTo(
      activated, options => options
        .Excluding(x =>
          x.Name == "Created"
          || (x.DeclaringType.IsAssignableTo(typeof(IJoin))
            && x.Name == "ActivationSide")
          || (x.DeclaringType.IsAssignableTo(typeof(IJoin))
            && x.Name == "ActivationId")
          || (x.DeclaringType.IsAssignableTo(typeof(ApiKeyModel))
            && x.Name == "Value")));
  }
}
