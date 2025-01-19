using Ozds.Business.Activation;
using Ozds.Business.Conversion;
using Ozds.Business.Extensions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Test.Conversion;

public class ModelEntityConverterTest
{
  public static readonly
    TheoryData<Type> TestData = new(
      AppDomain.CurrentDomain
        .GetAssemblies()
        .SelectMany(assembly => assembly
          .GetTypes()
          .Where(type =>
            !type.IsGenericType &&
            type.IsAssignableTo(typeof(IModel)))));

  [Theory]
  [MemberData(nameof(TestData))]
  public void Converts(Type modelType)
  {
    var serviceCollection = new ServiceCollection();
    serviceCollection.AddActivation();
    serviceCollection.AddConversion();

    var serviceProvider = serviceCollection.BuildServiceProvider();
    var activator = serviceProvider
      .GetRequiredService<ModelActivator>();
    var modelEntityConverter = serviceProvider
      .GetRequiredService<ModelEntityConverter>();

    var activationType = (TestData as IEnumerable<Type>)
      .FirstOrDefault(type =>
        !type.IsGenericType
        && type.IsAssignableTo(modelType))!;
    activationType.Should().NotBeNull();
    var activated = activator.ActivateDynamic(activationType);
    activated.Should().NotBeNull().And.BeOfType(activationType);

    var entityType = modelEntityConverter.EntityType(modelType);
    var entity = modelEntityConverter.ToEntity(activated);
    entity.Should().NotBeNull().And.BeAssignableTo(entityType);

    var converted = modelEntityConverter.ToModel(entity);
    converted.Should().NotBeNull().And.BeAssignableTo(modelType);
    converted.Should().BeEquivalentTo(activated);
  }
}
