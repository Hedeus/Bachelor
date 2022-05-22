using Bachelor.Infrastructure.Commands.Base;
using System.Windows;

namespace Bachelor.Infrastructure.Commands
{
    internal class CloseWindow : Command
    {
        protected override bool CanExecute(object parameter) => (parameter as Window ?? App.FocusedWindow ?? App.ActiveWindow) != null
        protected override void Execute(object parameter) => (parameter as Window ?? App.FocusedWindow ?? App.ActiveWindow)?.Close();
    }
}
