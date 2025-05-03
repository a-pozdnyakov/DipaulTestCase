using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Reactive;
using DipaulTestCase.Models.Interfaces;
using System.Threading.Tasks;

namespace DipaulTestCase.Models;

public class PhoneButton : ReactiveObject, IPhoneButton
{
    private char _symbol;
    public char Symbol
    {
        get => _symbol;
        set => this.RaiseAndSetIfChanged(ref _symbol, value);
    }

    private string _hint;
    public string Hint
    {
        get => _hint;
        set => this.RaiseAndSetIfChanged(ref _hint, value);
    }

    private ObservableCollection<char> _altSymbols;
    public ObservableCollection<char> AltSymbols
    {
        get => _altSymbols;
        set => this.RaiseAndSetIfChanged(ref _altSymbols, value);
    }

    private bool _isSendButton;
    public bool IsSendButton
    {
        get => _isSendButton;
        set => this.RaiseAndSetIfChanged(ref _isSendButton, value);
    }

    private bool _isEraseButton;
    public bool IsEraseButton
    {
        get => _isEraseButton;
        set => this.RaiseAndSetIfChanged(ref _isEraseButton, value);
    }

    private readonly ReactiveCommand<Unit, Task> _buttonPressedCommand;
    public ReactiveCommand<Unit, Task> ButtonPressedCommand => _buttonPressedCommand;

    public PhoneButton(char symbol, string hint, Func<IPhoneButton, Task>? buttonPressedAction = null, IEnumerable<char>? altSymbols = null, bool isSendButton = false, bool isEraseButton = false)
    {
        _symbol = symbol;
        _hint = hint;
        _altSymbols = altSymbols == null
            ? []
            : [.. altSymbols];
        _isSendButton = isSendButton;
        _isEraseButton = isEraseButton;

        _buttonPressedCommand = ReactiveCommand.Create(async () =>
        {
            if (buttonPressedAction != null)
            {
                await buttonPressedAction.Invoke(this);
            }
        });
    }
}
