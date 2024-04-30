using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCXEditor.Interfaces
{
    public interface IDialogService
    {
        Task<string> ShowOpenFolderDialog(string directory);
    }
}
