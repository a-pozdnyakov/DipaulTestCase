using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;

namespace DipaulTestCase.Models.Interfaces
{
    public interface IPhoneButton
    {
        public char Symbol { get; }
        public string Hint { get; }
        public ObservableCollection<char> AltSymbols { get; }
        public bool IsSendButton { get; }
        public bool IsEraseButton { get; }
        public ReactiveCommand<Unit, Task> ButtonPressedCommand { get; }
    }
}