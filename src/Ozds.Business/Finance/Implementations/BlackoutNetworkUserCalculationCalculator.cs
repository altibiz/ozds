using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries;

namespace Ozds.Business.Finance.Implementations;

public class BlackoutNetworkUserCalculationCalculator(
  ClockQueries clock,
  TimeQueries time
)
  : INetworkUserCalculationCalculator
{

  public bool CanCalculate(
    NetworkUserCalculationBasisModel calculationBasis
  )
  {
    return calculationBasis.Aggregates
      .Select(x => time.GetStartOfMonth(x.Timestamp))
      .Distinct()
      .Count() < 2;
  }

  public NetworkUserCalculationModel Calculate(
    NetworkUserCalculationBasisModel calculationBasis
  )
  {
    var usageCatalogue = calculationBasis.UsageNetworkUserCatalogue;

    var total = 0.0M;

    var now = clock.Timestamp();

    var initial = new BlackoutNetworkUserCalculationModel
    {
      Id = default!,
      Title =
        $"{usageCatalogue.Title} calculation for "
        + $"{calculationBasis.NetworkUser.Title} at "
        + $"{calculationBasis.Location.Title}",
      MeterId = calculationBasis.Meter.Id,
      ToDate = calculationBasis.BilledToDate,
      FromDate = calculationBasis.BilledFromDate,
      RequestedFromDate = calculationBasis.FromDate,
      RequestedToDate = calculationBasis.ToDate,
      MeteredFromDate = calculationBasis.MeasuredFromDate,
      MeteredToDate = calculationBasis.MeasuredToDate,
      NetworkUserInvoiceId = "0",
      UsageNetworkUserCatalogueId = usageCatalogue.Id,
      SupplyRegulatoryCatalogueId =
        calculationBasis.SupplyRegulatoryCatalogue.Id,
      NetworkUserMeasurementLocationId =
        calculationBasis.MeasurementLocation.Id,
      Remark = calculationBasis.MeasurementLocation.CalculationRemark,
      IssuedOn = now,
      IssuedById = default!,
      ArchivedMeter = calculationBasis.Meter,
      ArchivedNetworkUserMeasurementLocation =
        calculationBasis.MeasurementLocation,
      ConcreteArchivedUsageNetworkUserCatalogue = usageCatalogue,
      ArchivedSupplyRegulatoryCatalogue =
        calculationBasis.SupplyRegulatoryCatalogue,
      Total_EUR = total
    };

    return initial;
  }
}
