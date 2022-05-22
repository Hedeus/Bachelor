using Bachelor.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Bachelor.Services
{
    internal static  class ServicesRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IUserDialog, UserDialogService>()
        ;
    }
}
