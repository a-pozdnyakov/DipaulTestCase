using DipaulTestCase.Models;
using DipaulTestCase.Models.Interfaces;
using DipaulTestCase.Services;
using DipaulTestCase.Services.Interfaces;
using DipaulTestCase.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DipaulTestCase
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services
                .AddTransient<PhoneViewModel>()
                .AddTransient<PhoneButtonViewModel>()
                .AddTransient<IPhone, Phone>()
                .AddTransient<IPhoneButton, PhoneButton>()
                .AddSingleton<IPhoneInputSequenceProcessorFactory, PhoneInputSequenceProcessorFactory>();

            return services;
        }
    }
}