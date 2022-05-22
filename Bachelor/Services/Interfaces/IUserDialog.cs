using System.Collections.Generic;

namespace Bachelor.Services.Interfaces
{
    internal interface IUserDialog
    {
        bool OpenFile(string Title, out string SelectedFile, string Filter = "Все файлы (*.*)|*.*");

        bool OpenFiles(string Title, out IEnumerable<string> SelectedFiles, string Filter = "Все файлы (*.*)|*.*");
    }
}