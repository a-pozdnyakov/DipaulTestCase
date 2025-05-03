using DipaulTestCase.Models;
using DipaulTestCase.Models.Interfaces;
using ReactiveUI;

namespace DipaulTestCase.ViewModels
{
    public class PhoneButtonViewModel : ViewModelBase
    {
        private IPhoneButton _phoneButton;
        public IPhoneButton PhoneButton
        {
            get => _phoneButton;
            set => this.RaiseAndSetIfChanged(ref _phoneButton, value);
        }

        public PhoneButtonViewModel(IPhoneButton phoneButton)
        {
            _phoneButton = phoneButton;
        }
    }
}