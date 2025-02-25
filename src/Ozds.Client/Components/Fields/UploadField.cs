using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations;
using Ozds.Client.Components.Base;
using Ozds.Client.Conversion;
using Ozds.Client.Extensions;
using Ozds.Client.Import;

namespace Ozds.Client.Components.Fields;

public partial class UploadField : OzdsComponentBase
{
  [Inject]
  private IServiceProvider ServiceProvider { get; set; } = null!;

  [Parameter]
  public Type RecordType { get; set; } = default!;

  private readonly List<string> _fileNames = new();
  private readonly List<Stream> _fileStreams = new();
  private MudFileUpload<IReadOnlyList<IBrowserFile>>? _fileUpload;

  private async Task ClearAsync()
  {
    if (_fileUpload is not null)
    {
      await _fileUpload.ClearAsync();
    }
    _fileNames.Clear();
    _fileStreams.Clear();
  }

  private Task OpenFilePickerAsync()
  {
    return _fileUpload?.OpenFilePickerAsync() ?? Task.CompletedTask;
  }

  private void OnInputFileChanged(InputFileChangeEventArgs e)
  {
    var files = e.GetMultipleFiles();
    foreach (var file in files)
    {
      _fileStreams.Add(file.OpenReadStream());
      _fileNames.Add(file.Name);
    }
  }

  private async Task Upload()
  {
    var importer = ServiceProvider.GetRequiredService<CsvImporter>();
    var mutations = ScopedServices.GetRequiredService<AuditableMutations>();
    var mutationsMeasurement = ScopedServices.GetRequiredService<MeasurementMutations>();
    var converter = ScopedServices.GetRequiredService<ModelRecordConverter>();
    foreach (var stream in _fileStreams)
    {
      if (stream == null)
      {
        continue;
      }

      using var streamer = importer.Import(RecordType, stream, CancellationToken);
      await foreach (var recordsChunk in streamer.Stream().Chunk(CancellationToken))
      {
        var models = recordsChunk
          .Where(record => record is not null)
          .Select(record => converter.ToModel(record!)!)
          .ToList();
        // TODO: Make bulck operations
        foreach (var model in models)
        {
          if (model is IAuditable auditable)
          {
            await mutations.Create(auditable, CancellationToken);
          }
          else if (model is IMeasurement measurement)
          {
            await mutationsMeasurement.CreateMeasurements(new[] { measurement }, CancellationToken);
          }
        }
      }
    }
  }
}
