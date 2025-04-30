using Avalonia.Threading;
using DipaulTestCase.Dtos;
using ReactiveUI;
using System;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;

namespace DipaulTestCase.ViewModels;

public class PhoneButtonViewModel : ViewModelBase
{
    //private DispatcherTimer _timerInsertSpace = new(DispatcherPriority.Default);
    private CancellationTokenSource? _buttonPressedCts = null;

    private readonly PhoneDigitButtonDto _phoneDigitButton;

    public char ButtonDigit => _phoneDigitButton.Digit;
    public string ButtonHintText => _phoneDigitButton.HintText!;

    public ReactiveCommand<Unit, Unit> ButtonPressedCommand { get; }

    public PhoneButtonViewModel(PhoneDigitButtonDto phoneDigitButton, DispatcherTimer timerInsertSpaceToSequence, Action<char> buttonPressedCommandAction)
    {
        _phoneDigitButton = phoneDigitButton;

        //_timerInsertSpace.Interval = TimeSpan.FromSeconds(1);
        //_timerInsertSpace.Tick += (o, e) => {
        //    _timerInsertSpace.Stop();
        //    buttonPressedCommandAction.Invoke(' ');
        //};

        ButtonPressedCommand = ReactiveCommand.Create(() =>
        {
            timerInsertSpaceToSequence.Stop();

            //_buttonPressedCts?.Cancel();
            //_buttonPressedCts?.Dispose();

            //_buttonPressedCts = new CancellationTokenSource();
            //CancellationToken token = _buttonPressedCts.Token;

            //Task.Run(async () =>
            //{
            //    await Task.Delay(1000);

            //    buttonPressedCommandAction.Invoke(' ', token);
            //}, token);

            buttonPressedCommandAction.Invoke(ButtonDigit);

            timerInsertSpaceToSequence.Start();
        });
    }
}
