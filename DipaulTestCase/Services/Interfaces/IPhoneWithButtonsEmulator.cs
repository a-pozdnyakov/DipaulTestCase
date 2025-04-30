using DipaulTestCase.Dtos;
using System.Collections.Generic;

namespace DipaulTestCase.Services.Interfaces;

public interface IPhoneWithButtonsEmulator
{
    public Dictionary<char, PhoneDigitButtonDto> DigitButtons { get; }
    public char LineSeparatorChar { get; }
    public char EraseChar { get; }

    void ReinitButtons(IEnumerable<PhoneDigitButtonDto> mapping, char lineSeparatorChar, char eraseChar);
    string ProcessSequence(string? sequence);
}
