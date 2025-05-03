using Avalonia.ReactiveUI;
using DipaulTestCase.Converters;
using DipaulTestCase.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace DipaulTestCase.Views
{
    public partial class PhoneView : ReactiveUserControl<PhoneViewModel>
    {
        public PhoneView()
        {
            InitializeComponent();

            this.WhenActivated(disposableRegistration =>
            {
                this.OneWayBind(
                    ViewModel,
                    vm => vm.Phone.MessageText,
                    v => v.tbPhoneMessageText.Text
                )
                .DisposeWith(disposableRegistration);

                this.OneWayBind(
                    ViewModel,
                    vm => vm.PhoneButtons,
                    v => v.icPhoneButtons.ItemsSource
                )
                .DisposeWith(disposableRegistration);

                this.Bind(
                    ViewModel,
                    vm => vm.IsMessageHistoryShown,
                    v => v.tbtToggleMessageHistory.IsChecked
                )
                .DisposeWith(disposableRegistration);

                this.OneWayBind(
                    ViewModel,
                    vm => vm.SentMessagesCount,
                    v => v.tbMessageCount.Text
                )
                .DisposeWith(disposableRegistration);

                this.OneWayBind(
                    ViewModel,
                    vm => vm.Phone.SentMessages,
                    v => v.lbSentMessages.ItemsSource
                )
                .DisposeWith(disposableRegistration);

                this.OneWayBind(
                    ViewModel,
                    vm => vm.IsMessageHistoryShown,
                    v => v.lbSentMessages.IsVisible
                )
                .DisposeWith(disposableRegistration);

                this.OneWayBind(
                    ViewModel,
                    vm => vm.IsMessageHistoryShown,
                    v => v.icPhoneButtons.IsVisible,
                    vmToViewConverterOverride: new BoolToReverseBoolConverter()
                )
                .DisposeWith(disposableRegistration);

                tbPhoneMessageText.Focus();
            });
        }
    }
}