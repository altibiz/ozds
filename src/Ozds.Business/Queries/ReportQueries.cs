using System.Globalization;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;
using Ozds.Business.Naming;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Entities.Composite;
using DataReportQueries = Ozds.Data.Queries.ReportQueries;

namespace Ozds.Business.Queries;

public class ReportQueries(
  DataReportQueries dataReportQueries,
  LocalizationQueries localizationQueries,
  ModelEntityConverter modelEntityConverter,
  MeterNamingConvention meterNamingConvention
) : IQueries
{
  public async Task<List<EnergyCardReportModel>?> ReadEnergyCardReports(
    CultureInfo culture,
    IEnumerable<string> measurementLocationIds,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    CancellationToken cancellationToken
  )
  {
    var entities = await dataReportQueries.ReadEnergyCardReportBasis(
      measurementLocationIds,
      fromDate,
      toDate,
      cancellationToken);
    if (entities is null)
    {
      return null;
    }

    return entities
      .Select(entity => MakeEnergyCardReport(entity, culture))
      .ToList();
  }

  public async Task<List<EnergyCardReportModel>?>
    ReadEnergyCardReportsByNetworkUser(
      CultureInfo culture,
      string networkUserId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    var entities = await dataReportQueries
      .ReadEnergyCardReportBasisByNetworkUser(
        networkUserId,
        fromDate,
        toDate,
        cancellationToken);
    if (entities is null)
    {
      return null;
    }

    return entities
      .Select(entity => MakeEnergyCardReport(entity, culture))
      .ToList();
  }

  public async Task<List<EnergyCardReportModel>?>
    ReadEnergyCardReportsByLocation(
      CultureInfo culture,
      string locationId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    var entities = await dataReportQueries.ReadEnergyCardReportBasisByLocation(
      locationId,
      fromDate,
      toDate,
      cancellationToken);
    if (entities is null)
    {
      return null;
    }

    return entities
      .Select(entity => MakeEnergyCardReport(entity, culture))
      .ToList();
  }

  public async Task<List<AccountingPeriodReportModel>?>
    ReadAccountingPeriodReports(
      CultureInfo culture,
      string measurementLocationId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    var entity = await dataReportQueries.ReadAccountingPeriodReportBasis(
      measurementLocationId,
      fromDate,
      toDate,
      cancellationToken);
    if (entity is null)
    {
      return null;
    }

    return MakeAccountingPeriodReports(entity);
  }

  public async Task<List<AccountingPeriodReportModel>?>
    ReadAccountingPeriodReportsByMeter(
      CultureInfo culture,
      string meterId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    var modelType = meterNamingConvention.AggregateTypeForMeterId(meterId);
    var entityType = modelEntityConverter.EntityType(modelType);

    var entity = await dataReportQueries.ReadAccountingPeriodReportBasisByMeter(
      entityType,
      meterId,
      fromDate,
      toDate,
      cancellationToken);
    if (entity is null)
    {
      return null;
    }

    return MakeAccountingPeriodReports(entity);
  }

  public async Task<List<LoadCurveReportModel>?> ReadLoadCurveReports(
    CultureInfo culture,
    string measurementLocationId,
    ObisModel obis,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    CancellationToken cancellationToken
  )
  {
    var entity = await dataReportQueries.ReadLoadCurveReportBasis(
      measurementLocationId,
      fromDate,
      toDate,
      cancellationToken);
    if (entity is null)
    {
      return null;
    }

    return MakeLoadCurveReports(entity, obis);
  }

  public async Task<List<LoadCurveReportModel>?> ReadLoadCurveReportsByMeter(
    CultureInfo culture,
    string meterId,
    ObisModel obis,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    CancellationToken cancellationToken
  )
  {
    var modelType = meterNamingConvention.AggregateTypeForMeterId(meterId);
    var entityType = modelEntityConverter.EntityType(modelType);

    var entity = await dataReportQueries.ReadLoadCurveReportBasisByMeter(
      entityType,
      meterId,
      fromDate,
      toDate,
      cancellationToken);
    if (entity is null)
    {
      return null;
    }

    return MakeLoadCurveReports(entity, obis);
  }

  private EnergyCardReportModel MakeEnergyCardReport(
    EnergyCardReportBasisEntity entity,
    CultureInfo culture
  )
  {
    var model = new EnergyCardReportBasisModel
    {
      Location = modelEntityConverter
        .ToModel<LocationModel>(entity.Location),
      NetworkUser = modelEntityConverter
        .ToModel<NetworkUserModel>(entity.NetworkUser),
      Catalogue = modelEntityConverter
        .ToModel<NetworkUserCatalogueModel>(entity.Catalogue),
      MeasurementLocation = modelEntityConverter
        .ToModel<NetworkUserMeasurementLocationModel>(
          entity.MeasurementLocation),
      Meter = modelEntityConverter
        .ToModel<MeterModel>(entity.Meter),
      MinAggregate = modelEntityConverter
        .ToModel<AggregateModel>(entity.MinAggregate),
      MaxAggregate = modelEntityConverter
        .ToModel<AggregateModel>(entity.MaxAggregate)
    };

    var obis = model.Catalogue.Obis.ToList();

    var report = new EnergyCardReportModel
    {
      SocialSecurityNumber = model.NetworkUser.LegalPerson.SocialSecurityNumber,
      NetworkUserTitle = model.NetworkUser.Title,
      MeasurementLocationCode = GetMeasurementLocationCode(model),
      TariffModel = localizationQueries
        .Translate(culture, model.Catalogue.GetType()),
      ConnectionPower_W = model.Meter.ConnectionPower_W,
      MeasurementLocationTitle = model.MeasurementLocation.Title,
      LocationTitle = model.Location.Title,
      LocationAddress = model.Location.LegalPerson.Address,
      LocationCity = model.Location.LegalPerson.City,
      LocationPostalCode = model.Location.LegalPerson.PostalCode,
      Year = model.MinAggregate.Timestamp.Year.ToString(),
      BillingPeriod =
        model.MinAggregate.Timestamp.Month.ToString().PadLeft(2, '0'),
      ActiveEnergyTotalImportT1_kWh =
        obis.Contains(ObisModel.ActiveEnergyTotalImportT1_kWh)
          ? ObisModel.ActiveEnergyTotalImportT1_kWh.GetValue(
            model.MinAggregate,
            model.MaxAggregate)
          : null,
      ActiveEnergyTotalImportT2_kWh =
        obis.Contains(ObisModel.ActiveEnergyTotalImportT2_kWh)
          ? ObisModel.ActiveEnergyTotalImportT2_kWh.GetValue(
            model.MinAggregate,
            model.MaxAggregate)
          : null,
      ReactiveEnergyTotalImportT0_kVARh =
        obis.Contains(ObisModel.ReactiveEnergyTotalImportT0_kVARh)
          ? ObisModel.ReactiveEnergyTotalImportT0_kVARh.GetValue(
            model.MinAggregate,
            model.MaxAggregate)
          : null,
      ReactiveEnergyTotalExportT0_kVARh =
        obis.Contains(ObisModel.ReactiveEnergyTotalExportT0_kVARh)
          ? ObisModel.ReactiveEnergyTotalExportT0_kVARh.GetValue(
            model.MinAggregate,
            model.MaxAggregate)
          : null,
      ActivePowerTotalImportT1_kW =
        obis.Contains(ObisModel.ActivePowerTotalImportT1_kW)
          ? ObisModel.ActivePowerTotalImportT1_kW.GetValue(
            model.MinAggregate,
            model.MaxAggregate)
          : null
    };

    return report;
  }

  private List<AccountingPeriodReportModel> MakeAccountingPeriodReports(
    AccountingPeriodReportBasisEntity entity
  )
  {
    var model = new AccountingPeriodReportBasisModel
    {
      Location = modelEntityConverter
        .ToModel<LocationModel>(entity.Location),
      NetworkUser = modelEntityConverter
        .ToModel<NetworkUserModel>(entity.NetworkUser),
      Catalogue = modelEntityConverter
        .ToModel<NetworkUserCatalogueModel>(entity.Catalogue),
      MeasurementLocation = modelEntityConverter
        .ToModel<NetworkUserMeasurementLocationModel>(
          entity.MeasurementLocation),
      Meter = modelEntityConverter.ToModel<MeterModel>(entity.Meter),
      MinAggregate = modelEntityConverter
        .ToModel<AggregateModel>(entity.MinAggregate),
      MaxAggregate = modelEntityConverter
        .ToModel<AggregateModel>(entity.MaxAggregate)
    };

    var obis = new List<ObisModel>
    {
      ObisModel.ActiveEnergyTotalImportT1_kWh,
      ObisModel.ActiveEnergyTotalImportT2_kWh,
      ObisModel.ReactiveEnergyTotalImportT0_kVARh,
      ObisModel.ReactiveEnergyTotalExportT0_kVARh,
      ObisModel.ActivePowerTotalImportT1_kW
    };

    return obis
      .Select(
        obis => new AccountingPeriodReportModel
        {
          MeasurementLocationCode = GetMeasurementLocationCode(model),
          Timestamp = model.MinAggregate.Timestamp,
          ObisCode = obis.ToCode(),
          Unit = obis.ToUnit(),
          Value = obis.GetValue(model.MinAggregate)
        })
      .Concat(
        obis
          .Select(
            obis => new AccountingPeriodReportModel
            {
              MeasurementLocationCode = GetMeasurementLocationCode(model),
              Timestamp = model.MaxAggregate.Timestamp,
              ObisCode = obis.ToCode(),
              Unit = obis.ToUnit(),
              Value = obis.GetValue(model.MaxAggregate)
            }))
      .ToList();
  }

  private List<LoadCurveReportModel> MakeLoadCurveReports(
    LoadCurveReportBasisEntity entity,
    ObisModel obis
  )
  {
    var model = new LoadCurveReportBasisModel
    {
      Location = modelEntityConverter
        .ToModel<LocationModel>(entity.Location),
      NetworkUser = modelEntityConverter
        .ToModel<NetworkUserModel>(entity.NetworkUser),
      Catalogue = modelEntityConverter
        .ToModel<NetworkUserCatalogueModel>(entity.Catalogue),
      MeasurementLocation = modelEntityConverter
        .ToModel<NetworkUserMeasurementLocationModel>(
          entity.MeasurementLocation),
      Meter = modelEntityConverter
        .ToModel<MeterModel>(entity.Meter),
      Aggregates = entity.Aggregates
        .Select(modelEntityConverter.ToModel<AggregateModel>)
        .ToList()
    };

    return model.Aggregates
      .Select(
        aggregate => new LoadCurveReportModel
        {
          MeasurementLocationCode = GetMeasurementLocationCode(model),
          Timestamp = aggregate.Timestamp,
          ObisCode = obis.ToCode(),
          MeterId = model.Meter.Id,
          Energy_kx = obis.GetValue(aggregate),
          Power_kx = obis.GetDerivedValue(aggregate)
        })
      .ToList();
  }

  private static string GetMeasurementLocationCode(
    ReportBasisModel basis
  )
  {
    return
      basis.Location.Id.PadLeft(3, '0')
      + basis.MeasurementLocation.Id.PadLeft(9, '0');
  }
}
