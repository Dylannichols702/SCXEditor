using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using SCXEditor.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCXEditor.Services
{
    internal class DialogService : IDialogService
    {
        public async Task<string> ShowOpenFolderDialog(string directory)
        {
            var path = string.Empty;
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
                && desktop.MainWindow is not null)
            {
                var results = await desktop.MainWindow.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions());
            }

            return string.Empty;
        }
    }
}
