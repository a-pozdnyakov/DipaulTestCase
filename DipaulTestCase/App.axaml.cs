using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DipaulTestCase.ViewModels;
using DipaulTestCase.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DipaulTestCase
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            ServiceCollection collection = new();

            collection
                .AddCommonServices();

            IServiceProvider services = collection.BuildServiceProvider();
            PhoneEmulatorViewModel vm = services.GetRequiredService<PhoneEmulatorViewModel>();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = vm
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new PhoneEmulatorView
                {
                    DataContext = vm
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}