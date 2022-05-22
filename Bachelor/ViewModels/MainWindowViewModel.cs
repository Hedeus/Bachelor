using Bachelor.Infrastructure.Commands.Base;
using Bachelor.Services.Interfaces;
using Bachelor.ViewModels.Base;
using System.IO;
using System.Windows.Input;
using System.Windows.Markup;

namespace Bachelor.ViewModels
{
    [MarkupExtensionReturnType(typeof(MainWindowViewModel))]
    internal class MainWindowViewModel : ViewModel
    {
        /*---------------------------------------------Properies------------------------------------------------*/
        #region Свойства

        private readonly IUserDialog _UserDialog;

        #region Title : string - Заголовок вікна
        private string _Title = "Бакалаврська робота";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Password : string - Пароль
        private string _Password = "123";

        public string Password { get => _Password; set => Set(ref _Password, value); }
        #endregion

        #region SelectedFile : FileInfo - Выбранный файл
        private FileInfo _SelectedFile;
        public FileInfo SelectedFile { get => _SelectedFile; set => Set(ref _SelectedFile, value); }
        #endregion

        #endregion

        /*---------------------------------------------Commands------------------------------------------------*/

        #region Команды

        #region SelectedFileCommand - команда выбора файла
        private ICommand _SelectedFileCommand;
        public ICommand SelectedFileCommand => _SelectedFileCommand ??= new LambdaCommand(OnSelectedFileCommandExecuted);

        private void OnSelectedFileCommandExecuted()
        {
            if (!_UserDialog.OpenFile("Выбор файла для шифрования", out var file_path)) return;
            var selected_file = new FileInfo(file_path);
            SelectedFile = selected_file.Exists ? selected_file : null;
        }
        #endregion

        private ICommand _EncrypCommand;
        public ICommand EncrypCommand => _EncrypCommand ??= new LambdaCommand(OnEncrypCommandExecuted, CanEncrypCommandExecute);

        private bool CanEncrypCommandExecute(object p) => (p is FileInfo file && file.Exists || SelectedFile != null) && !string.IsNullOrWhiteSpace(Password);

        private void OnEncrypCommandExecuted(object p)
        {
            var file = p as FileInfo ?? SelectedFile;
            if (file is null) return;
        }

        private ICommand _DecrypCommand;
        public ICommand DecrypCommand => _DecrypCommand ??= new LambdaCommand(OnDecrypCommandExecuted, CanDecrypCommandExecute);

        private bool CanDecrypCommandExecute(object p) => (p is FileInfo file && file.Exists || SelectedFile != null) && !string.IsNullOrWhiteSpace(Password);

        private void OnDecrypCommandExecuted(object p)
        {
            var file = p as FileInfo ?? SelectedFile;
            if (file is null) return;
        }

        #endregion

        public MainWindowViewModel(IUserDialog UserDialog)
        {
            _UserDialog = UserDialog;
        }

    }
}
