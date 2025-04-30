using Avalonia.Metadata;
using DipaulTestCase.Dtos;
using DipaulTestCase.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DipaulTestCase.Services;

public sealed class PhoneWithButtonsEmulator : IPhoneWithButtonsEmulator
{
    private readonly StringBuilder _builder = new();

    public Dictionary<char, PhoneDigitButtonDto> DigitButtons { get; private set; } = new() {
        { '1', new PhoneDigitButtonDto('1') },
        { '2', new PhoneDigitButtonDto('2', "abc", ['A', 'B', 'C'] ) },
        { '3', new PhoneDigitButtonDto('3', "def", ['D', 'E', 'F'] ) },
        { '4', new PhoneDigitButtonDto('4', "ghi", ['G', 'H', 'I'] ) },
        { '5', new PhoneDigitButtonDto('5', "jkl", ['J', 'K', 'L'] ) },
        { '6', new PhoneDigitButtonDto('6', "mno", ['M', 'N', 'O'] ) },
        { '7', new PhoneDigitButtonDto('7', "pqrs", ['P', 'Q', 'R', 'S'] ) },
        { '8', new PhoneDigitButtonDto('8', "tuv", ['T', 'U', 'V'] ) },
        { '9', new PhoneDigitButtonDto('9', "wxyz", ['W', 'X', 'Y', 'Z'] ) },
        { '*', new PhoneDigitButtonDto('*', "⌫") },
        { '0', new PhoneDigitButtonDto('0', "␣", [' ']) },
        { '#', new PhoneDigitButtonDto('#', "⇛") }
    };

    public char LineSeparatorChar { get; private set; } = '#';
    public char EraseChar { get; private set; } = '*';

    public PhoneWithButtonsEmulator()
    {
    }

    public void ReinitButtons(IEnumerable<PhoneDigitButtonDto> mapping, char lineSeparatorChar, char eraseChar)
    {
        DigitButtons = mapping.ToDictionary(
            k => k.Digit,
            v => v
        );

        LineSeparatorChar = lineSeparatorChar;
        EraseChar = eraseChar;
    }

    public string ProcessSequence(string? sequence)
    {
        if (string.IsNullOrWhiteSpace(sequence))
        {
            return string.Empty;
        }

        List<string> sentLines = [];

        foreach (string sentLine in sequence.Split(LineSeparatorChar))
        {
            sentLines.Add(
                processSentLine(sentLine)
            );
        }

        string result = string.Join(Environment.NewLine, sentLines);

        sentLines.Clear();
        sentLines.TrimExcess();

        return result;
    }

    private string processSentLine(string line)
    {
        _builder.Clear();
        char? lastProcessedChar = null;
        short charRepeatedHits = 0;

        foreach (char processingChar in line)
        {
            if (lastProcessedChar == processingChar)
            {
                charRepeatedHits++;
            }
            else
            {
                addOutputCharIfNeeded(lastProcessedChar, charRepeatedHits);

                lastProcessedChar = processingChar;
                charRepeatedHits = 0;
            }
        }

        addOutputCharIfNeeded(lastProcessedChar, charRepeatedHits);

        return _builder.ToString();
    }

    private void addOutputCharIfNeeded(char? c, short repeatedHits)
    {
        if (!c.HasValue)
        {
            return;
        }

        short totalHits = (short)(repeatedHits + 1);

        if (c == EraseChar)
        {
            if (_builder.Length > repeatedHits)
            {
                _builder.Length -= totalHits;
            }
            else
            {
                _builder.Clear();
            }

            return;
        }

        if (DigitButtons.TryGetValue(c.Value, out PhoneDigitButtonDto? button) && button.AltSymbols != null)
        {
            if (button.AltSymbols.Count == 1)
            {
                _builder.Append(string.Concat(Enumerable.Repeat(button.AltSymbols.First(), totalHits)));
            }
            else
            {
                //using carousel principle (start over if number of hits exceeds collection size)
                int index = repeatedHits % button.AltSymbols.Count;
                _builder.Append(button.AltSymbols[index]);
            }
        }
    }
}
