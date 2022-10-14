using PhlegmaticOne.Crypto.ClassicCrypto.Core.Extension;
using PhlegmaticOne.Crypto.ClassicCrypto.Core.LettersEncyption;
using PhlegmaticOne.Crypto.ClassicCrypto.PolybiusSquare.Alphabet;

namespace PhlegmaticOne.Crypto.ClassicCrypto.PolybiusSquare.LettersEncryption;

public class ReadPolybiusEncryptedDigitCodesByRowsLetterEncryptionPolicy : ILetterEncryptionPolicy
{
    private readonly List<(int, int)> _initialValues;
    private readonly List<(int, int)> _readValues;
    private readonly SquareAlphabet _squareAlphabet;
    private int _currentEncryptIndex;
    private int _currentDecryptIndex;
    public ReadPolybiusEncryptedDigitCodesByRowsLetterEncryptionPolicy(SquareAlphabet squareAlphabet)
    {
        _readValues = new();
        _initialValues = new();
        _squareAlphabet = squareAlphabet;
    }

    public string EncryptLetter(char letter)
    {
        var (col, row) = _readValues[_currentEncryptIndex];
        _currentEncryptIndex++;
        return _squareAlphabet[row, col].ToString();
    }

    public char DecryptLetter(string from)
    {
        var (col, row) = _initialValues[_currentDecryptIndex];
        _currentDecryptIndex++;
        return _squareAlphabet[row, col];
    }

    public void PreEncryptAction(string stringToEncrypt)
    {
        _currentEncryptIndex = 0;

        var letterIndexes = GetStringLetterIndexes(stringToEncrypt);

        var initialLetterIndexes = new string(letterIndexes);

        var readByRows = initialLetterIndexes.Where((x, i) => i.IsOdd())
            .Concat(initialLetterIndexes.Where((x, i) => i.IsOdd() == false))
            .ToArray();
        var readLetterIndexes = new string(readByRows);

        FillValues(readLetterIndexes, _readValues);
    }

    public void PreDecryptAction(string stringToDecrypt)
    {
        _currentDecryptIndex = 0;

        var letterIndexes = GetStringLetterIndexes(stringToDecrypt);

        var result = new char[letterIndexes.Length];

        for(var i = 0; i < letterIndexes.Length / 2; i++)
        {
            result[2 * i] = letterIndexes[i];
        }
        for(var i = letterIndexes.Length / 2; i < letterIndexes.Length; i++)
        {
            result[2 * (i - letterIndexes.Length / 2) + 1] = letterIndexes[i];
        }

        var str = new string(result);

        FillValues(str, _initialValues);
    }

    private char[] GetStringLetterIndexes(string str)
    {
        var letterIndexes = str.SelectMany(x =>
        {
            var (row, col) = _squareAlphabet[x];
            return col + row.ToString();
        }).ToArray();

        return letterIndexes.ToArray();
    }

    private static void FillValues(string str, List<(int, int)> values)
    {
        for (var i = 0; i < str.Length / 2; i++)
        {
            var first = Convert.ToInt32(str[2 * i].ToString());
            var second = Convert.ToInt32(str[2 * i + 1].ToString());
            values.Add((first, second));
        }
    }
}
