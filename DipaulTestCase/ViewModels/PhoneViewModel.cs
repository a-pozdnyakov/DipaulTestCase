using ReactiveUI;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using DipaulTestCase.Models.Interfaces;
using DipaulTestCase.Models;

namespace DipaulTestCase.ViewModels;

public class PhoneViewModel : ViewModelBase
{
    private IPhone _phone;
    public IPhone Phone
    {
        get => _phone;
        set => this.RaiseAndSetIfChanged(ref _phone, value);
    }

    private bool? _isMessageHistoryShown = false;
    public bool? IsMessageHistoryShown
    {
        get => _isMessageHistoryShown;
        set => this.RaiseAndSetIfChanged(ref _isMessageHistoryShown, value);
    }

    private readonly ObservableAsPropertyHelper<IEnumerable<PhoneButtonViewModel>> _phoneButtons;
    public IEnumerable<PhoneButtonViewModel> PhoneButtons => _phoneButtons.Value;

    private readonly ObservableAsPropertyHelper<int> _sentMessagesCount;
    public int SentMessagesCount => _sentMessagesCount.Value;

    #region Commands
    public ReactiveCommand<TextBox, Unit> RestoreTextBoxFocusCommand { get; }
    public ReactiveCommand<TextBox, Unit> SetTextBoxCaretPositionCommand { get; }
    #endregion

    public PhoneViewModel(IPhone phone)
    {
        _phone = phone;

        _phoneButtons = this
            .WhenAnyValue(t => t.Phone.PhoneButtons)
            .DistinctUntilChanged()
            .Select(getPhoneButtonViewModel)
            .ObserveOn(RxApp.MainThreadScheduler)
            .ToProperty(this, t => t.PhoneButtons);

        _sentMessagesCount = this
            .WhenAnyValue(t => t.Phone.SentMessages.Count)
            .ObserveOn(RxApp.MainThreadScheduler)
            .ToProperty(this, t => t.SentMessagesCount);

        #region Init Commands
        RestoreTextBoxFocusCommand = ReactiveCommand.Create<TextBox>((tbElement) =>
        {
            tbElement.Focus();
        });

        SetTextBoxCaretPositionCommand = ReactiveCommand.Create<TextBox>((tbElement) =>
        {
            tbElement.SelectionStart = tbElement.Text!.Length;
            tbElement.SelectionEnd = tbElement.Text.Length;
        });
        #endregion
    }

    private IEnumerable<PhoneButtonViewModel> getPhoneButtonViewModel(IEnumerable<IPhoneButton> buttons)
    {
        return buttons
            .Select(x => 
                new PhoneButtonViewModel(x)
            );
    }
}
