using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Ozds.Client.Export.Abstractions
{
  public interface IExporter
  {
    string Export(
      IEnumerable<object> models,
      CancellationToken cancellationToken = default
    );
    public string ExportGeneric<T>(
      IEnumerable<T> models,
      CancellationToken cancellationToken = default
    );
  }
}
