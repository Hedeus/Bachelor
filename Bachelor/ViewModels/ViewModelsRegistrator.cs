using Microsoft.Extensions.DependencyInjection;

namespace Bachelor.ViewModels
{
    internal static class ViewModelsRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
           .AddSingleton<MainWindowViewModel>()
        ;
            
        
    }
}
