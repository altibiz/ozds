using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using CsvHelper;
using Ozds.Business.Models.Abstractions;
using Ozds.Client.Conversion;
using Ozds.Client.Export.Abstractions;

namespace Ozds.Client.Export
{
  public class CsvExporter : IExporter
  {
    private readonly ModelRecordConverter _modelRecordConverter;

    public CsvExporter(ModelRecordConverter modelRecordConverter)
    {
      _modelRecordConverter = modelRecordConverter;
    }

    public string Export(
      IEnumerable<object> models,
      CancellationToken cancellationToken = default
    )
    {
      var modelList = models.ToList();
      if (!modelList.Any())
        return string.Empty;

      var records = modelList
        .Select(model => _modelRecordConverter.ToRecord(model))
        .ToList();

      using var stringWriter = new StringWriter();
      using var csvWriter = new CsvWriter(
        stringWriter,
        CultureInfo.InvariantCulture
      );
      csvWriter.WriteRecords((dynamic)records);
      csvWriter.Flush();
      return stringWriter.ToString();
    }

    public string ExportGeneric<T>(
      IEnumerable<T> models,
      CancellationToken cancellationToken = default
    )
    {
      var list = models.ToList();
      if (!list.Any())
        return string.Empty;

      using var stringWriter = new StringWriter();
      using var csvWriter = new CsvWriter(
        stringWriter,
        CultureInfo.InvariantCulture
      );
      csvWriter.WriteRecords(list);
      csvWriter.Flush();
      return stringWriter.ToString();
    }

    public CalculationAggregateBasisEntity ToCalculationBasis(IAggregate aggregate)
    {
      return new CalculationAggregateBasisEntity
      {
        MeasurementLocationId = aggregate.MeasurementLocationId,
        ActiveEnergyTotalImportT0Max_Wh = aggregate.ActiveEnergy_Wh
          .TariffUnary()
          .DuplexImport()
          .AggregateMax()
          .PhaseSum(),
        ActiveEnergyTotalImportT0Min_Wh = aggregate.ActiveEnergy_Wh
          .TariffUnary()
          .DuplexImport()
          .AggregateMin()
          .PhaseSum(),
        ActiveEnergyTotalImportT1Max_Wh = aggregate.ActiveEnergy_Wh
          .TariffBinary()
          .T1
          .DuplexImport()
          .AggregateMax()
          .PhaseSum(),
        ActiveEnergyTotalImportT1Min_Wh = aggregate.ActiveEnergy_Wh
          .TariffBinary()
          .T1
          .DuplexImport()
          .AggregateMin()
          .PhaseSum(),
        ActiveEnergyTotalImportT2Max_Wh = aggregate.ActiveEnergy_Wh
          .TariffBinary()
          .T2
          .DuplexImport()
          .AggregateMax()
          .PhaseSum(),
        ActiveEnergyTotalImportT2Min_Wh = aggregate.ActiveEnergy_Wh
          .TariffBinary()
          .T2
          .DuplexImport()
          .AggregateMin()
          .PhaseSum(),
        ReactiveEnergyTotalImportT0Max_VARh = aggregate.ReactiveEnergy_VARh
          .TariffUnary()
          .DuplexImport()
          .AggregateMax()
          .PhaseSum(),
        ReactiveEnergyTotalImportT0Min_VARh = aggregate.ReactiveEnergy_VARh
          .TariffUnary()
          .DuplexImport()
          .AggregateMin()
          .PhaseSum(),
        ReactiveEnergyTotalExportT0Max_VARh = aggregate.ReactiveEnergy_VARh
          .TariffUnary()
          .DuplexExport()
          .AggregateMax()
          .PhaseSum(),
        ReactiveEnergyTotalExportT0Min_VARh = aggregate.ReactiveEnergy_VARh
          .TariffUnary()
          .DuplexExport()
          .AggregateMin()
          .PhaseSum(),
        DerivedActivePowerTotalImportT1Max_W = aggregate.DerivedActivePower_W
          .TariffBinary()
          .T1
          .DuplexImport()
          .AggregateMax()
          .PhaseSum()
      };
    }

    public class CalculationAggregateBasisEntity
    {
      public string MeasurementLocationId { get; set; } = default!;

      public decimal ActiveEnergyTotalImportT0Min_Wh { get; set; } = default;

      public decimal ActiveEnergyTotalImportT0Max_Wh { get; set; } = default;

      public decimal ActiveEnergyTotalImportT1Min_Wh { get; set; } = default;

      public decimal ActiveEnergyTotalImportT1Max_Wh { get; set; } = default;

      public decimal ActiveEnergyTotalImportT2Min_Wh { get; set; } = default;

      public decimal ActiveEnergyTotalImportT2Max_Wh { get; set; } = default;

      public decimal ReactiveEnergyTotalImportT0Min_VARh { get; set; } = default;

      public decimal ReactiveEnergyTotalImportT0Max_VARh { get; set; } = default;

      public decimal ReactiveEnergyTotalExportT0Min_VARh { get; set; } = default;

      public decimal ReactiveEnergyTotalExportT0Max_VARh { get; set; } = default;

      public decimal DerivedActivePowerTotalImportT1Max_W { get; set; } = default!;
    }
  }
}
