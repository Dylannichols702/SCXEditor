using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCXEditor.Services
{
    public class FileService : IFileService
    {
        public static FilePickerFileType XDRV { get; } = new("XDRV Charts")
        {
            Patterns = new[] { "*.xdrv" },
        };

        public static FilePickerFileType AudioFiles { get; } = new("Audio File")
        {
            Patterns = new[] { "*.mp3", "*.wav", "*.ogg" },
        };

        private readonly Window _target;

        public FileService(Window target)
        {
            _target = target;
        }

        public async Task<IStorageFile?> OpenFileAsync(FilePickerFileType[] types)
        {
            var files = await _target.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
            {
                Title = "Open File",
                AllowMultiple = false,
                FileTypeFilter = types
            });

            return files.Count >= 1 ? files[0] : null;
        }

        public async Task<IStorageFolder?> OpenFolderAsync()
        {
            var folders = await _target.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
            {
                Title = "Open Folder",
                AllowMultiple = false
            });

            return folders.Count >= 1 ? folders[0] : null;
        }
    }
}
