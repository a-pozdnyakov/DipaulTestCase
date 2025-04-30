using Avalonia.Controls;
using Avalonia.ReactiveUI;
using DipaulTestCase.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace DipaulTestCase.Views;

public partial class PhoneEmulatorView : ReactiveUserControl<PhoneEmulatorViewModel>
{
    public PhoneEmulatorView()
    {
        InitializeComponent();

        this.WhenActivated(disposableRegistration =>
        {
            this.OneWayBind(
                ViewModel,
                vm => vm.PhoneMessageText,
                v => v.tbPhoneMessageText.Text
            )
            .DisposeWith(disposableRegistration);

            this.OneWayBind(
                ViewModel,
                vm => vm.PhoneButtons,
                v => v.icPhoneButtons.ItemsSource
            )
            .DisposeWith(disposableRegistration);
        });
    }
}
