using PhlegmaticOne.Crypto.ClassicCrypto.Core.LettersEncyption;
using PhlegmaticOne.Crypto.Core.Helpers;
using PhlegmaticOne.Crypto.PolybiusSquare.Alphabet;

namespace PhlegmaticOne.Crypto.PolybiusSquare.LettersEncryption;

public class OneRowDownEncryptionPolicy : ILetterEncryptionPolicy
{
    private readonly SquareAlphabet _squareAlphabet;

    public OneRowDownEncryptionPolicy(SquareAlphabet squareAlphabet) => _squareAlphabet = squareAlphabet;
 
    public char DecryptLetter(string from)
    {
        var ch = from[0];
        if(from.Length == 1 && ch == CharConstants.SPACE)
        {
            return ch;
        }
        var (row, col) = _squareAlphabet[ch];
        return _squareAlphabet[row == 0 ? _squareAlphabet.Rank - 1 : row - 1, col];
    }

    public string EncryptLetter(char letter)
    {
        if(letter == CharConstants.SPACE)
        {
            return letter.ToString();
        }
        var (row, col) = _squareAlphabet[letter];
        return _squareAlphabet[(row + 1) % _squareAlphabet.Rank, col].ToString();
    }

    public void PreEncryptAction(string _) { }

    public void PreDecryptAction(string _) { }
}
