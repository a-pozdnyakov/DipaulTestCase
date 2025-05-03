using DipaulTestCase.Models.Interfaces;
using System.Collections.Generic;

namespace DipaulTestCase.Services.Interfaces
{
    public interface IPhoneInputSequenceProcessorFactory
    {
        public IPhoneInputSequenceProcessor Create(IEnumerable<IPhoneButton> buttons);
    }
}