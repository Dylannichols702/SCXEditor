using Avalonia.Platform.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCXEditor.Services
{
    public interface IFileService
    {
        public Task<IStorageFolder?> OpenFolderAsync();
        public Task<IStorageFile?> OpenFileAsync(FilePickerFileType[] types);
    }
}
