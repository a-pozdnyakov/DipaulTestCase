using DipaulTestCase.Models;
using DipaulTestCase.Models.Interfaces;
using DipaulTestCase.Services;
using DipaulTestCase.Services.Interfaces;
using DipaulTestCase.ViewModels;
using System.Reactive.Linq;

namespace DipaulTestCase.Tests
{
    public class DigitSequenceProcessorTest
    {
        private readonly PhoneInputSequenceProcessor _processor;
        private readonly PhoneViewModel _phoneViewModel;
        private readonly IPhone _phone;
        private readonly IPhoneInputSequenceProcessorFactory _sequenceProcessorFactory;

        public DigitSequenceProcessorTest()
        {
            IEnumerable<IPhoneButton> phoneButtons = [
                new PhoneButton('1', string.Empty),
                new PhoneButton('2', "abc", altSymbols: ['A', 'B', 'C']),
                new PhoneButton('3', "def", altSymbols: ['D', 'E', 'F']),
                new PhoneButton('4', "ghi", altSymbols: ['G', 'H', 'I']),
                new PhoneButton('5', "jkl", altSymbols: ['J', 'K', 'L']),
                new PhoneButton('6', "mno", altSymbols: ['M', 'N', 'O']),
                new PhoneButton('7', "pqrs", altSymbols: ['P', 'Q', 'R', 'S']),
                new PhoneButton('8', "tuv", altSymbols: ['T', 'U', 'V']),
                new PhoneButton('9', "wxyz", altSymbols: ['W', 'X', 'Y', 'Z']),
                new PhoneButton('*', "⌫", isEraseButton: true),
                new PhoneButton('0', "␣", altSymbols: [' ']),
                new PhoneButton('#', "⇛", isSendButton: true)
            ];

            _processor = new PhoneInputSequenceProcessor(phoneButtons);

            _sequenceProcessorFactory = new PhoneInputSequenceProcessorFactory();
            _phone = new Phone(_sequenceProcessorFactory);
            _phoneViewModel = new PhoneViewModel(_phone);
        }

        [Fact]
        public async Task PhoneViewModelButtonPressingTest()
        {
            ReactiveUI.ReactiveCommand<System.Reactive.Unit, Task> command0 = _phone.PhoneButtons.Where(t => t.Symbol == '0').Select(t => t.ButtonPressedCommand).FirstOrDefault()!;
            ReactiveUI.ReactiveCommand<System.Reactive.Unit, Task> command2 = _phone.PhoneButtons.Where(t => t.Symbol == '2').Select(t => t.ButtonPressedCommand).FirstOrDefault()!;
            ReactiveUI.ReactiveCommand<System.Reactive.Unit, Task> command3 = _phone.PhoneButtons.Where(t => t.Symbol == '3').Select(t => t.ButtonPressedCommand).FirstOrDefault()!;
            ReactiveUI.ReactiveCommand<System.Reactive.Unit, Task> command4 = _phone.PhoneButtons.Where(t => t.Symbol == '4').Select(t => t.ButtonPressedCommand).FirstOrDefault()!;
            ReactiveUI.ReactiveCommand<System.Reactive.Unit, Task> command5 = _phone.PhoneButtons.Where(t => t.Symbol == '5').Select(t => t.ButtonPressedCommand).FirstOrDefault()!;
            ReactiveUI.ReactiveCommand<System.Reactive.Unit, Task> command6 = _phone.PhoneButtons.Where(t => t.Symbol == '6').Select(t => t.ButtonPressedCommand).FirstOrDefault()!;
            ReactiveUI.ReactiveCommand<System.Reactive.Unit, Task> command7 = _phone.PhoneButtons.Where(t => t.Symbol == '7').Select(t => t.ButtonPressedCommand).FirstOrDefault()!;
            ReactiveUI.ReactiveCommand<System.Reactive.Unit, Task> command8 = _phone.PhoneButtons.Where(t => t.Symbol == '8').Select(t => t.ButtonPressedCommand).FirstOrDefault()!;

            await command4.Execute();
            await command4.Execute();
            await Task.Delay(1000);
            await command4.Execute();
            await command4.Execute();
            await command4.Execute();
            await command0.Execute();
            await command4.Execute();
            await command4.Execute();
            await command4.Execute();
            await command8.Execute();
            await Task.Delay(1000);
            await command7.Execute();
            await command7.Execute();
            await command7.Execute();
            await command7.Execute();
            await command0.Execute();
            await command6.Execute();
            await command3.Execute();
            await command3.Execute();
            await Task.Delay(1000);
            await command0.Execute();
            await command6.Execute();
            await command2.Execute();
            await command7.Execute();
            await command7.Execute();
            await command7.Execute();
            await command4.Execute();
            await command4.Execute();
            await command4.Execute();
            await command6.Execute();
            await command6.Execute();
            await command6.Execute();

            Assert.Equal("HI ITS ME MARIO", _phoneViewModel.Phone.MessageText);
        }

        [Fact]
        public void IsPhoneButtonMappingValidTest()
        {
            Assert.Single(_phone.PhoneButtons, t => t.IsSendButton);
            Assert.Single(_phone.PhoneButtons, t => t.IsEraseButton);
            Assert.DoesNotContain(_phone.PhoneButtons, t => t.IsEraseButton && t.IsSendButton);
            Assert.Distinct(_phone.PhoneButtons.Select(t => t.Symbol));
        }

        [Theory]
        [InlineData("44 4445*#", "HI")]
        [InlineData("4433555 555666096667775553#", "HELLO WORLD")]
        [InlineData("34447288555#", "DIPAUL")]
        public void SequenceProcessorTest(string sequence, string expectedResult)
        {
            _processor.ProcessSequence(sequence, out string result);

            Assert.Equal(expectedResult, result.Trim());
        }
    }
}