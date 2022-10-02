namespace PhlegmaticOne.Crypto.Core.Alphabet;

public interface ILetterToDigitConverter : IEnumerable<KeyValuePair<char, int>>
{
    int Length { get; }
    int ConvertLetter(char letter);
    char ConvertDigit(int digit);
    bool Contains(char letter);
    IDictionary<char, int> GetLettersInDigitRange(int start, int end);
}