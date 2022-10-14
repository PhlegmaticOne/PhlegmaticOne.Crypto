using System.Collections;

namespace PhlegmaticOne.Crypto.Core.Alphabet;

public class LetterToDigitConverter : ILetterToDigitConverter
{
    private readonly IDictionary<char, int> _lettersToDigit;
    public LetterToDigitConverter(IDictionary<char, int> lettersToDigit) => _lettersToDigit = lettersToDigit;

    public static LetterToDigitConverter FromAlphabetString(string alphabetString)
    {
        var alphabet = alphabetString
            .Select((x, i) => (x, i))
            .ToDictionary(x => x.x, x => x.i);
        return new LetterToDigitConverter(alphabet);
    }
    public int Length => _lettersToDigit.Count;

    public bool Contains(char letter) => _lettersToDigit.ContainsKey(letter);

    public char ConvertDigit(int digit)
    {
        var result = char.MinValue;
        try
        {
            result = _lettersToDigit.Single(x => x.Value == digit).Key;
        }
        catch { }

        return result;
    }
    public IDictionary<char, int> GetLettersInDigitRange(int start, int end) =>
        _lettersToDigit.Where(x => x.Value >= start && x.Value < end).ToDictionary(x => x.Key, x => x.Value);
    public int ConvertLetter(char letter) => _lettersToDigit.TryGetValue(char.ToLower(letter), out int digit) ? digit : -1;

    public IEnumerator<KeyValuePair<char, int>> GetEnumerator() => _lettersToDigit.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}