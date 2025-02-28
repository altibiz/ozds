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

    [Inject]
    private IServiceProvider ServiceProvider { get; set; } = null!;

    private string GetDownloadUrl()
    {
      var exporter = ServiceProvider.GetRequiredService<CsvExporter>();
      if (Models.Any())
      {
        return "data:text/csv;charset=utf-8," + Uri.EscapeDataString(exporter.Export(Models));
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
