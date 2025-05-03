using System.Collections.Generic;

namespace DipaulTestCase.Dtos
{
    public sealed class PhoneButtonDto(char symbol, string hint, IEnumerable<char>? altSymbols = null, bool isSendButton = false, bool isEraseButton = false)
    {
        public char Symbol { get; private set; } = symbol;
        public string Hint { get; private set; } = hint;
        public IEnumerable<char> AltSymbols { get; private set; } = altSymbols ?? [];
        public bool IsSendButton { get; private set; } = isSendButton;
        public bool IsEraseButton { get; private set; } = isEraseButton;
    }
}