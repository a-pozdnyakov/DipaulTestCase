using ReactiveUI;
using System.Collections.ObjectModel;
using DipaulTestCase.Models.Interfaces;
using DipaulTestCase.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace DipaulTestCase.Models;

public class Phone : ReactiveObject, IPhone
{
    private readonly IPhoneInputSequenceProcessor _phoneInputSequenceProcessor;

    CancellationTokenSource? _ctsCancelAppendSpaceAction;

    private ObservableCollection<IPhoneButton> _phoneButtons;
    public ObservableCollection<IPhoneButton> PhoneButtons
    {
        get => _phoneButtons;
        private set => this.RaiseAndSetIfChanged(ref _phoneButtons, value);
    }

    private StringBuilder _digitSequence = new();
    public StringBuilder DigitSequence
    {
        get => _digitSequence;
        private set => this.RaiseAndSetIfChanged(ref _digitSequence, value);
    }

    private string? _messageText = string.Empty;
    public string? MessageText
    {
        get => _messageText;
        private set => this.RaiseAndSetIfChanged(ref _messageText, value);
    }

    private ObservableCollection<string> _sentMessages = [];
    public ObservableCollection<string> SentMessages
    {
        get => _sentMessages;
        private set => this.RaiseAndSetIfChanged(ref _sentMessages, value);
    }

    public Phone(IPhoneInputSequenceProcessorFactory phoneInputSequenceProcessorFactory)
    {
        _phoneButtons = [
            new PhoneButton('1', string.Empty),
            new PhoneButton('2', "abc", buttonPressedAction, ['A', 'B', 'C']),
            new PhoneButton('3', "def", buttonPressedAction, ['D', 'E', 'F']),
            new PhoneButton('4', "ghi", buttonPressedAction, ['G', 'H', 'I']),
            new PhoneButton('5', "jkl", buttonPressedAction, ['J', 'K', 'L']),
            new PhoneButton('6', "mno", buttonPressedAction, ['M', 'N', 'O']),
            new PhoneButton('7', "pqrs", buttonPressedAction, ['P', 'Q', 'R', 'S']),
            new PhoneButton('8', "tuv", buttonPressedAction, ['T', 'U', 'V']),
            new PhoneButton('9', "wxyz", buttonPressedAction, ['W', 'X', 'Y', 'Z']),
            new PhoneButton('*', "⌫", buttonPressedAction, isEraseButton: true),
            new PhoneButton('0', "␣", buttonPressedAction, [' ']),
            new PhoneButton('#', "⇛", buttonPressedAction, isSendButton: true)
        ];

        _phoneInputSequenceProcessor = phoneInputSequenceProcessorFactory.Create(_phoneButtons);
    }

    private void appendToDigitSequenceAction(object newData)
    {
        DigitSequence.Append(newData);
        processDigitsSequence();
    }

    private async Task appendSpaceAction(CancellationToken token)
    {
        await Task.Delay(1000, token);

        if (token.IsCancellationRequested)
        {
            return;
        }

        appendToDigitSequenceAction(' ');
    }

    private async Task buttonPressedAction(IPhoneButton button)
    {
        CancellationToken token = resetSpaceAppendingToken();

        appendToDigitSequenceAction(button.Symbol);
        await appendSpaceAction(token);
    }

    private void processDigitsSequence()
    {
        if (_phoneInputSequenceProcessor.ProcessSequence(DigitSequence.ToString(), out string output))
        {
            resetSpaceAppendingToken();

            SentMessages.Add(output);
            DigitSequence.Length = 0;
            MessageText = string.Empty;

            return;
        }

        MessageText = output;
    }

    private CancellationToken resetSpaceAppendingToken()
    {
        _ctsCancelAppendSpaceAction?.Cancel();
        _ctsCancelAppendSpaceAction?.Dispose();

        _ctsCancelAppendSpaceAction = new CancellationTokenSource();

        return _ctsCancelAppendSpaceAction.Token;
    }
}
