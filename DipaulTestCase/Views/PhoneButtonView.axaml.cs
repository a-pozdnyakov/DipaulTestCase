using Avalonia.ReactiveUI;
using DipaulTestCase.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace DipaulTestCase.Views
{
    public partial class PhoneButtonView : ReactiveUserControl<PhoneButtonViewModel>
    {
        public PhoneButtonView()
        {
            InitializeComponent();

            this.WhenActivated(disposableRegistration =>
            {
                this.OneWayBind(
                    ViewModel,
                    vm => vm.PhoneButton.Symbol,
                    v => v.btDigitButton.Content
                )
                .DisposeWith(disposableRegistration);

                this.OneWayBind(
                    ViewModel,
                    vm => vm.PhoneButton.Hint,
                    v => v.btDigitButton.Tag
                )
                .DisposeWith(disposableRegistration);

                this
                    .BindCommand(
                        ViewModel,
                        vm => vm.PhoneButton.ButtonPressedCommand,
                        v => v.btDigitButton
                    )
                    .DisposeWith(disposableRegistration);
            });
        }
    }
}