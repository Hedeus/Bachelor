using Microsoft.Extensions.DependencyInjection;

namespace Bachelor.ViewModels
{
    internal class ViewModelsLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();


    }
}
