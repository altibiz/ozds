using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Ozds.Business.Mutations;
using Ozds.Client.Components.Base;
using Ozds.Client.Components.Dialogs;

namespace Ozds.Client.Components.Fields;

public partial class UploadField : OzdsComponentBase
{
  private readonly List<string> _fileNames = new();
  private readonly List<Stream> _fileStreams = new();
  private MudFileUpload<IReadOnlyList<IBrowserFile>>? _fileUpload;

  [Inject]
  private IDialogService DialogService { get; set; } = default!;

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

  private async Task Import()
  {
    try
    {
      var mutations = ScopedServices.GetRequiredService<ReportMutations>();
      foreach (
        var (stream, name) in _fileStreams
          .OfType<Stream>()
          .Zip(_fileNames, (stream, name) => (stream, name))
      )
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
    catch (Exception ex)
    {
      await DialogService.ShowAsync<MutatingResult>(
        Translate("Failure"),
        new DialogParameters
        {
          {
            nameof(MutatingResult.Body),
            $"{Translate("Failed importing")}"
              + $" {Translate(Type)} - {ex.Message}"
          },
        },
        new DialogOptions { CloseOnEscapeKey = true }
      );
      return;
    }

    await DialogService.ShowAsync<MutatingResult>(
      Translate("Success"),
      new DialogParameters
      {
        {
          nameof(MutatingResult.Body),
          $"{Translate("Successfully imported")} {Translate(Type)}"
        },
      },
      new DialogOptions { CloseOnEscapeKey = true }
    );
  }
}
