using DipaulTestCase.Models.Interfaces;
using DipaulTestCase.Services.Interfaces;
using System.Collections.Generic;

namespace DipaulTestCase.Services
{
    public sealed class PhoneInputSequenceProcessorFactory : IPhoneInputSequenceProcessorFactory
    {
        public IPhoneInputSequenceProcessor Create(IEnumerable<IPhoneButton> buttons) => new PhoneInputSequenceProcessor(buttons);
    }
}