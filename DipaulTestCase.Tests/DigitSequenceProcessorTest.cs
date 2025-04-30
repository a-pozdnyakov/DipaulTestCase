using DipaulTestCase.Services;
using DipaulTestCase.Services.Interfaces;

namespace DipaulTestCase.Tests
{
    public class DigitSequenceProcessorTest
    {
        private readonly IPhoneWithButtonsEmulator _processor;

        public DigitSequenceProcessorTest()
        {
            _processor = new PhoneWithButtonsEmulator();
        }

        [Theory]
        [InlineData("44 4445*#", "HI")]
        [InlineData("4433555 555666096667775553#", "HELLO WORLD")]
        [InlineData("34447288555#", "DIPAUL")]
        public void DigitProcessor(string sequence, string expectedResult)
        {
            string result = _processor.ProcessSequence(sequence);

            Assert.Equal(expectedResult, result.Trim());
        }
    }
}