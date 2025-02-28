using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using CsvHelper;
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
  }
}
