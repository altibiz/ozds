using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Ozds.Business.Models;
using Ozds.Business.Mutations;
using Ozds.Client.Import;
using Ozds.Client.Import.Abstractions;
using Ozds.Client.Import.ModelsImport;
using static Ozds.Client.Import.ModelsImport.NetworkUserImporter;

namespace Ozds.Client.Pages;

public partial class NetworkUsersPage
{
  // Inject the service provider
  [Inject]
  private IServiceProvider ServiceProvider { get; set; } = null!;
  private readonly List<string> _fileNames = new();
  private readonly List<Stream> _fileStreams = new();
  private const string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full";
  private string _dragClass = DefaultDragClass;
  private MudFileUpload<IReadOnlyList<IBrowserFile>>? _fileUpload;

  private async Task ClearAsync()
  {
    await (_fileUpload?.ClearAsync() ?? Task.CompletedTask);
    _fileNames.Clear();
    _fileStreams.Clear();
    ClearDragClass();
  }

  private Task OpenFilePickerAsync()
      => _fileUpload?.OpenFilePickerAsync() ?? Task.CompletedTask;

  private void OnInputFileChanged(InputFileChangeEventArgs e)
  {
    ClearDragClass();
    var files = e.GetMultipleFiles();
    foreach (var file in files)
    {
      _fileStreams.Add(file.OpenReadStream());
      _fileNames.Add(file.Name);
    }
  }

  async Task Upload()
  {
    var csvParser = ServiceProvider.GetRequiredService<ICsvParser>();
    var networkUserImporter = ServiceProvider.GetRequiredService<INetworkUserImporter>();
    var mutations = ScopedServices.GetRequiredService<AuditableMutations>();
    foreach (var stream in _fileStreams)
    {
      if (stream != null)
      {
        var networkUsers = await csvParser.ParseAndMapCsvAsync<NetworkUserCsvRecord, NetworkUserModel>(stream, networkUserImporter.MapNetworkUsersAsync);
        foreach (var user in networkUsers)
        {
          await mutations.Create(user, CancellationToken);
        }
      }
    }
  }
  private void SetDragClass()
      => _dragClass = $"{DefaultDragClass} mud-border-primary";

  private void ClearDragClass()
      => _dragClass = DefaultDragClass;
}
