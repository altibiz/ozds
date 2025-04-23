using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Ozds.Business.Mutations;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Fields;

public partial class UploadField : OzdsComponentBase
{
  private readonly List<string> _fileNames = new();
  private readonly List<Stream> _fileStreams = new();
  private MudFileUpload<IReadOnlyList<IBrowserFile>>? _fileUpload;

  [Parameter]
  public Type Type { get; set; } = default!;

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
    var mutations = ScopedServices.GetRequiredService<ReportMutations>();
    foreach (var (stream, name) in _fileStreams
      .OfType<Stream>()
      .Zip(_fileNames, (stream, name) => (stream, name)))
    {
      await mutations.Import(
        name,
        GetCulture(),
        Type,
        stream,
        CancellationToken
      );
    }
  }
}
