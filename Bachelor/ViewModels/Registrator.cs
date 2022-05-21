using Microsoft.Extensions.DependencyInjection;

namespace Bachelor.ViewModels
{
    internal static class Registrator
    {
        public static IServiceCollection RegisterViewModel(this IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();

            return services;
        }
    }
}
