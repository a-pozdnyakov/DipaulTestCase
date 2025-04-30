using System.Collections.Generic;

namespace DipaulTestCase.Dtos
{
    public sealed class PhoneDigitButtonDto(char digit, string? hintText = null, List<char>? altSymbols = null)
    {
        public char Digit { get; private set; } = digit;
        public string? HintText { get; private set; } = hintText ?? string.Empty;
        public List<char>? AltSymbols { get; private set; } = altSymbols;
    }
}