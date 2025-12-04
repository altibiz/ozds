using System.Globalization;
using Ozds.Business.Finance.Implementations;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries;
using Ozds.Time.Queries.Abstractions;

namespace Ozds.Business.Test.Finance;

public class BlackoutNetworkUserCalculationCalculatorTest
{
  public static IEnumerable<BlackoutNetworkUserCalculationModel> TestData()
  {
    return new Fixture()
      .Customize(
        new TypeRelay(typeof(IAggregate), typeof(AbbB2xAggregateModel))
          .ToCustomization())
      .Customize(
        new TypeRelay(typeof(AggregateModel), typeof(AbbB2xAggregateModel))
          .ToCustomization())
      .Customize(
        new TypeRelay(typeof(IMeter), typeof(AbbB2xMeterModel))
          .ToCustomization())
      .Customize(
        new TypeRelay(typeof(MeterModel), typeof(AbbB2xMeterModel))
          .ToCustomization())
      .Customize(
        new TypeRelay(
            typeof(NetworkUserCatalogueModel),
            typeof(BlueLowNetworkUserCatalogueModel))
          .ToCustomization())
      .Build<BlackoutNetworkUserCalculationModel>()
      .CreateMany(Constants.DefaultFuzzCount)
      .Select(
        x =>
        {
          x.UsageNetworkUserCatalogueId =
            x.ConcreteArchivedUsageNetworkUserCatalogue.Id;
          x.SupplyRegulatoryCatalogueId =
            x.ArchivedSupplyRegulatoryCatalogue.Id;
          x.NetworkUserMeasurementLocationId =
            x.ArchivedNetworkUserMeasurementLocation.Id;
          x.Remark =
            x.ArchivedNetworkUserMeasurementLocation.CalculationRemark;
          x.MeterId = x.ArchivedMeter.Id;

          x.Total_EUR = 0.0M;

          return x;
        });
  }

  [Test]
  [MethodDataSource(nameof(TestData))]
  public void CalculatesCorrectlyWithFuzzyAbbB2xMeter(
    BlackoutNetworkUserCalculationModel expected)
  {
    var clockQueriesMock = new Mock<ClockQueries>(
      MockBehavior.Loose,
      Mock.Of<IClockQueries>());

    clockQueriesMock
      .Setup(x => x.Timestamp())
      .Returns(
        DateTimeOffset.Parse(
          "2000-01-01T00:00:00Z",
          CultureInfo.InvariantCulture));

    var timeQueriesMock = new Mock<TimeQueries>(
      MockBehavior.Loose,
      Mock.Of<ITimeQueries>());

    var calculator = new BlackoutNetworkUserCalculationCalculator(
      clockQueriesMock.Object,
      timeQueriesMock.Object);

    var fixture = new Fixture()
      .Customize(
        new TypeRelay(typeof(IAggregate), typeof(AbbB2xAggregateModel))
          .ToCustomization())
      .Customize(
        new TypeRelay(typeof(AggregateModel), typeof(AbbB2xAggregateModel))
          .ToCustomization())
      .Customize(
        new TypeRelay(typeof(IMeter), typeof(AbbB2xMeterModel))
          .ToCustomization())
      .Customize(
        new TypeRelay(typeof(MeterModel), typeof(AbbB2xMeterModel))
          .ToCustomization());

    var basis = fixture
      .Build<NetworkUserCalculationBasisModel>()
      .With(x => x.FromDate, expected.RequestedFromDate)
      .With(x => x.ToDate, expected.RequestedToDate)
      .With(x => x.BilledFromDate, expected.FromDate)
      .With(x => x.BilledToDate, expected.ToDate)
      .With(x => x.MeasuredFromDate, expected.MeteredFromDate)
      .With(x => x.MeasuredToDate, expected.MeteredToDate)
      .With(
        x => x.MeasurementLocation,
        expected.ArchivedNetworkUserMeasurementLocation)
      .With(
        x => x.SupplyRegulatoryCatalogue,
        expected.ArchivedSupplyRegulatoryCatalogue)
      .With(
        x => x.UsageNetworkUserCatalogue,
        expected.ConcreteArchivedUsageNetworkUserCatalogue)
      .With(x => x.Meter, expected.ArchivedMeter)
      .Create();

    var actual = calculator.Calculate(basis);

    actual.Should()
      .BeOfType<BlackoutNetworkUserCalculationModel>().And
      .BeEquivalentTo(
        expected,
        c => c
          .Excluding(x => x.Id)
          .Excluding(x => x.Title)
          .Excluding(x => x.NetworkUserInvoiceId)
          .Excluding(x => x.IssuedOn)
          .Excluding(x => x.IssuedById)
          .Excluding(x => x.Created));
  }

  [Test]
  public void CanCalculateReturnsTrueWhenDataIsInsufficient()
  {
    var timeQueriesMock = new Mock<TimeQueries>(
      MockBehavior.Strict,
      Mock.Of<ITimeQueries>());

    var fixedStartOfMonth = DateTimeOffset.UtcNow;
    timeQueriesMock
      .Setup(x => x.GetStartOfMonth(It.IsAny<DateTimeOffset>()))
      .Returns(fixedStartOfMonth);

    var clockQueriesMock = new Mock<ClockQueries>(
      MockBehavior.Loose,
      Mock.Of<IClockQueries>());

    var calculator = new BlackoutNetworkUserCalculationCalculator(
      clockQueriesMock.Object,
      timeQueriesMock.Object);

    var fixture = new Fixture()
      .Customize(
        new TypeRelay(typeof(IAggregate), typeof(AbbB2xAggregateModel))
          .ToCustomization())
      .Customize(
        new TypeRelay(typeof(AggregateModel), typeof(AbbB2xAggregateModel))
          .ToCustomization())
      .Customize(
        new TypeRelay(
            typeof(NetworkUserCatalogueModel),
            typeof(BlueLowNetworkUserCatalogueModel))
          .ToCustomization());

    var basis = fixture.Build<NetworkUserCalculationBasisModel>()
      .Create();

    var result = calculator.CanCalculate(basis);

    result.Should().BeTrue("because distinct months < 2");
  }

  [Test]
  public void CanCalculateReturnsFalseWhenDataIsSufficient()
  {
    var timeQueriesMock = new Mock<TimeQueries>(
      MockBehavior.Strict,
      Mock.Of<ITimeQueries>());

    timeQueriesMock
      .Setup(x => x.GetStartOfMonth(It.IsAny<DateTimeOffset>()))
      .Returns<DateTimeOffset>(d => d);

    var clockQueriesMock = new Mock<ClockQueries>(
      MockBehavior.Loose,
      Mock.Of<IClockQueries>());

    var calculator = new BlackoutNetworkUserCalculationCalculator(
      clockQueriesMock.Object,
      timeQueriesMock.Object);

    var fixture = new Fixture()
      .Customize(
        new TypeRelay(typeof(IAggregate), typeof(AbbB2xAggregateModel))
          .ToCustomization())
      .Customize(
        new TypeRelay(typeof(AggregateModel), typeof(AbbB2xAggregateModel))
          .ToCustomization())
      .Customize(
        new TypeRelay(
            typeof(NetworkUserCatalogueModel),
            typeof(BlueLowNetworkUserCatalogueModel))
          .ToCustomization());

    var basis = fixture.Build<NetworkUserCalculationBasisModel>()
      .Create();

    var result = calculator.CanCalculate(basis);

    result.Should().BeFalse("because distinct months >= 2");
  }
}
