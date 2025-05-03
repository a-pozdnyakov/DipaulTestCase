using System.Collections.ObjectModel;
using System.Text;

namespace DipaulTestCase.Models.Interfaces
{
    public interface IPhone
    {
        public ObservableCollection<IPhoneButton> PhoneButtons { get; }
        public StringBuilder DigitSequence { get; }
        public ObservableCollection<string> SentMessages { get; }
        public string? MessageText { get; }
    }
}