using Bachelor.Infrastructure.Commands.Base;
using Bachelor.Services.Interfaces;
using Bachelor.ViewModels.Base;
using System;
using System.Diagnostics;
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

        private const string __EncryptedFileSuffix = ".encrypted";

        private readonly IUserDialog _UserDialog;
        private readonly IEncryptor _Encryptor;

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

        private async void OnEncrypCommandExecuted(object p)
        {
            var file = p as FileInfo ?? SelectedFile;
            if (file is null) return;

            var default_file_name = file.FullName + __EncryptedFileSuffix;
            if (!_UserDialog.SaveFile("Выбор файла для сохранения", out var destination_path, default_file_name)) return;

            var timer = Stopwatch.StartNew();            
            ((Command)EncrypCommand).Executable = false;
            ((Command)DecrypCommand).Executable = false;  
           
            try
            {
                await _Encryptor.EncryptAsync(file.FullName, destination_path, Password);
            }
            catch (OperationCanceledException)
            {
                //throw;
            }

            ((Command)EncrypCommand).Executable = true;
            ((Command)DecrypCommand).Executable = true;
            timer.Stop();

            _UserDialog.Information("Шифрование", $"Шифрование файла успешно завершено за {timer.Elapsed.TotalSeconds:0.##} с");
        }

        private ICommand _DecrypCommand;
        public ICommand DecrypCommand => _DecrypCommand ??= new LambdaCommand(OnDecrypCommandExecuted, CanDecrypCommandExecute);

        private bool CanDecrypCommandExecute(object p) => (p is FileInfo file && file.Exists || SelectedFile != null) && !string.IsNullOrWhiteSpace(Password);

        private async void OnDecrypCommandExecuted(object p)
        {
            var file = p as FileInfo ?? SelectedFile;
            if (file is null) return;

            var default_file_name = file.FullName.EndsWith(__EncryptedFileSuffix)
                ? file.FullName.Substring(0, file.FullName.Length - __EncryptedFileSuffix.Length)
                : file.FullName;
            if (!_UserDialog.SaveFile("Выбор файла для сохранения", out var destination_path, default_file_name)) return;

            var timer = Stopwatch.StartNew();
            ((Command)EncrypCommand).Executable = false;
            ((Command)DecrypCommand).Executable = false;
            var decryption_task = _Encryptor.DecryptAsync(file.FullName, destination_path, Password);

            bool success = false;
            try
            {
                success = await decryption_task;
            }
            catch (OperationCanceledException)
            {

                //throw;
            }

            ((Command)EncrypCommand).Executable = true;
            ((Command)DecrypCommand).Executable = true;
            timer.Stop();

            if (success)
                _UserDialog.Information("Шифрование",  $"Дешифровка файла выполнена успешно за {timer.Elapsed.TotalSeconds:0.##} с");
            else
                _UserDialog.Warning("Шифрование", "Ошибка при дешифровке файла: указан неверный пароль");
        }

        #endregion

        public MainWindowViewModel(IUserDialog UserDialog, IEncryptor Encryptor)
        {
            _UserDialog = UserDialog;
            _Encryptor = Encryptor;
        }

    }
}
