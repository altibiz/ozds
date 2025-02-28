using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
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

    private string GetDownloadUrl()
    {
      var exporter = ServiceProvider.GetRequiredService<CsvExporter>();
      var csv = exporter.ExportGeneric(Models);
      if (!IsGeneric)
      {
        csv = exporter.Export(Models);
      }
      if (Models.Any())
      {
        return "data:text/csv;charset=utf-8," + Uri.EscapeDataString(csv);
      }
      return "";
    }

    private bool IsDownloadDisabled()
    {
      return !Models.Any();
    }

    private Dictionary<string, object> GetDownloadAttributes()
    {
      return new Dictionary<string, object>
            {
                { "download", FileName }
            };
    }
  }
}
