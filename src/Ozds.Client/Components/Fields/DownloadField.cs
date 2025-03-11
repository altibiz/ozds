using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Ozds.Business.Models.Abstractions;
using Ozds.Client.Components.Base;
using Ozds.Client.Export;
using Ozds.Client.Export.Abstractions;

namespace Ozds.Client.Components.Fields
{
  public partial class DownloadField : OzdsComponentBase
  {
    [Parameter]
    public IEnumerable<object> Models { get; set; } = Array.Empty<object>();

    [Parameter]
    public string FileName { get; set; } = "export.csv";
    [Parameter]
    public bool IsGeneric { get; set; } = true;

    [Inject]
    private IServiceProvider ServiceProvider { get; set; } = null!;

    [Inject]
    private IJSRuntime JS { get; set; } = null!;

    private async Task DownloadFileAsync()
    {
      var exporter = ServiceProvider.GetRequiredService<CsvExporter>();
      var csv = exporter.ExportGeneric(Models);
      if (Models is List<IAggregate> aggregates)
      {
        var newList = aggregates.Select(x => exporter.ToCalculationBasis(x));
        csv = exporter.ExportGeneric(newList);
      }
      else if (!IsGeneric)
      {
        csv = exporter.Export(Models);
      }
      if (Models.Any())
      {
        var bytes = Encoding.UTF8.GetBytes(csv);
        using var stream = new MemoryStream(bytes);
        using var streamRef = new DotNetStreamReference(stream: stream);
        await JS.InvokeVoidAsync("downloadFileFromStream", FileName, streamRef);
      }
    }

    private bool IsDownloadDisabled()
    {
      return !Models.Any();
    }
  }
}
