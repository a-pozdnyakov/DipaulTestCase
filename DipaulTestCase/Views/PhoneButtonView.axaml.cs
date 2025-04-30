using Avalonia.ReactiveUI;
using DipaulTestCase.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace DipaulTestCase.Views;

public partial class PhoneButtonView : ReactiveUserControl<PhoneButtonViewModel>
{
    public PhoneButtonView()
    {
        InitializeComponent();

        this.WhenActivated(disposableRegistration =>
        {
            this.OneWayBind(
                ViewModel,
                vm => vm.ButtonDigit,
                v => v.btDigitButton.Content
            )
            .DisposeWith(disposableRegistration);

            this.OneWayBind(
                ViewModel,
                vm => vm.ButtonHintText,
                v => v.btDigitButton.Tag
            )
            .DisposeWith(disposableRegistration);

            this
                .BindCommand(
                    ViewModel,
                    vm => vm.ButtonPressedCommand,
                    v => v.btDigitButton
                )
                .DisposeWith(disposableRegistration);
        });
    }
}
