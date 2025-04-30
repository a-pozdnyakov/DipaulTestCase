using DipaulTestCase.Services.Interfaces;
using ReactiveUI;
using System.Reactive.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Threading;
using DipaulTestCase.Dtos;

namespace DipaulTestCase.ViewModels;

public class PhoneEmulatorViewModel : ViewModelBase
{
    private readonly IPhoneWithButtonsEmulator _phoneWithButtonsEmulator;
    private readonly DispatcherTimer _timerInsertSpaceToSequence = new(DispatcherPriority.Default);

    private string _digitSequence = string.Empty;
    public string DigitSequence
    {
        get => _digitSequence;
        set => this.RaiseAndSetIfChanged(ref _digitSequence, value);
    }

    //public Dictionary<char, PhoneDigitButton> DigitButtons
    //{
    //    get => _phoneWithButtonsEmulator.DigitButtons;
    //    set => this.RaiseAndSetIfChanged(ref _phoneWithButtonsEmulator.DigitButtons, value);
    //}

    private readonly ObservableAsPropertyHelper<string?> _phoneMessageText;
    public string? PhoneMessageText => _phoneMessageText.Value;


    private readonly ObservableAsPropertyHelper<IEnumerable<PhoneButtonViewModel>> _phoneButtons;
    public IEnumerable<PhoneButtonViewModel> PhoneButtons => _phoneButtons.Value;

    public PhoneEmulatorViewModel(IPhoneWithButtonsEmulator phoneWithButtonsEmulator)
    {
        _phoneWithButtonsEmulator = phoneWithButtonsEmulator;

        _phoneButtons = this
            .WhenAnyValue(t => t._phoneWithButtonsEmulator.DigitButtons)
            .DistinctUntilChanged()
            .Select(getPhoneViewModel)
            .ObserveOn(RxApp.MainThreadScheduler)
            .ToProperty(this, t => t.PhoneButtons);

        _phoneMessageText = this
            .WhenAnyValue(t => t.DigitSequence)
            .DistinctUntilChanged()
            .Select(processDigitsSequence)
            .ObserveOn(RxApp.MainThreadScheduler)
            .ToProperty(this, t => t.PhoneMessageText);

        _timerInsertSpaceToSequence.Interval = TimeSpan.FromSeconds(1);
        _timerInsertSpaceToSequence.Tick += (o, e) =>
        {
            _timerInsertSpaceToSequence.Stop();
            DigitSequence = $"{_digitSequence} ";
        };
    }

    private string processDigitsSequence(string? sequence)
    {
        return _phoneWithButtonsEmulator.ProcessSequence(sequence);
    }

    private IEnumerable<PhoneButtonViewModel> getPhoneViewModel(Dictionary<char, PhoneDigitButtonDto> buttons)
    {
        return buttons
            .Select(x => 
                new PhoneButtonViewModel(
                    x.Value,
                    _timerInsertSpaceToSequence,
                    (digitChar) => {
                        DigitSequence = $"{_digitSequence}{digitChar}";
                    }
                )
            );
    }
}
