using DipaulTestCase.Models.Interfaces;
using DipaulTestCase.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DipaulTestCase.Services
{
    public sealed class PhoneInputSequenceProcessor : IPhoneInputSequenceProcessor
    {
        private readonly Dictionary<char, IEnumerable<char>> _commonCharsMapping = [];
        private readonly char _senderChar = '#';
        private readonly char _eraserChar = '*';

        public PhoneInputSequenceProcessor(IEnumerable<IPhoneButton> buttons)
        {
            foreach (IPhoneButton button in buttons)
            {
                if (button.IsSendButton)
                {
                    _senderChar = button.Symbol;

                    continue;
                }

                if (button.IsEraseButton)
                {
                    _eraserChar = button.Symbol;

                    continue;
                }

                if (button.AltSymbols.Any())
                {
                    _commonCharsMapping[button.Symbol] = [.. button.AltSymbols];
                }
            }
        }

        public bool ProcessSequence(string? sequence, out string output)
        {
            if (string.IsNullOrWhiteSpace(sequence))
            {
                output = string.Empty;

                return false;
            }

            StringBuilder builder = new();
            char? lastProcessedChar = null;
            short charRepeatedHits = 0;
            bool isOutputFinalized = false;

            foreach (char processingChar in sequence)
            {
                if (lastProcessedChar == processingChar)
                {
                    charRepeatedHits++;
                }
                else
                {
                    addOutputCharIfNeeded(builder, lastProcessedChar, charRepeatedHits);

                    lastProcessedChar = processingChar;
                    charRepeatedHits = 0;
                }

                if (processingChar == _senderChar)
                {
                    isOutputFinalized = true;

                    break;
                }
            }

            addOutputCharIfNeeded(builder, lastProcessedChar, charRepeatedHits);

            output = builder.ToString();

            builder.Clear();

            return isOutputFinalized;
        }

        private void addOutputCharIfNeeded(StringBuilder builder, char? c, short repeatedHits)
        {
            if (!c.HasValue || c == _senderChar)
            {
                return;
            }

            short totalHits = (short)(repeatedHits + 1);

            if (c == _eraserChar)
            {
                if (builder.Length > repeatedHits)
                {
                    builder.Length -= totalHits;
                }
                else
                {
                    builder.Clear();
                }

                return;
            }

            if (_commonCharsMapping.TryGetValue(c.Value, out IEnumerable<char>? altSymbols))
            {
                int altSymbolsCount = altSymbols.Count();

                if (altSymbolsCount == 1)
                {
                    builder.Append(string.Concat(Enumerable.Repeat(altSymbols.First(), totalHits)));
                }
                else
                {
                    //using carousel principle (start over if number of hits exceeds collection size)
                    int index = repeatedHits % altSymbolsCount;
                    builder.Append(altSymbols.ElementAt(index));
                }
            }
        }
    }
}