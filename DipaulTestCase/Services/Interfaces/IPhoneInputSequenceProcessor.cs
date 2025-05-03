namespace DipaulTestCase.Services.Interfaces
{
    public interface IPhoneInputSequenceProcessor
    {
        public bool ProcessSequence(string? sequence, out string output);
    }
}