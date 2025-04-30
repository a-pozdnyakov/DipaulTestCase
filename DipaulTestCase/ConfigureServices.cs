using DipaulTestCase.Services;
using DipaulTestCase.Services.Interfaces;
using DipaulTestCase.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DipaulTestCase
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection collection)
        {
            collection
                .AddTransient<PhoneEmulatorViewModel>()
                .AddTransient<PhoneButtonViewModel>()
                .AddTransient<IPhoneWithButtonsEmulator, PhoneWithButtonsEmulator>();

            return collection;
        }
    }
}