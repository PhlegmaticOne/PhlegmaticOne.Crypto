using PhlegmaticOne.Crypto.Core.Alphabet;
using PhlegmaticOne.Crypto.Core.Base;

namespace PhlegmaticOne.Crypto.Symmetric.Polynomial.EncryptionData;

public class PolynomialAlgorithmEncryptionData : IEncryptionData
{
    public ILetterToDigitConverter Alphabet { get; }
    public Func<int, int> Polinomial { get; }
    public int Mod { get; set; }
    public char SeparateEncryptingLettersChar { get; set; }
    public PolynomialAlgorithmEncryptionData(ILetterToDigitConverter alphabet, Func<int, int> polinomial, int mod, char separateEncryptingLettersChar)
    {
        Alphabet = alphabet;
        Polinomial = polinomial;
        Mod = mod;
        SeparateEncryptingLettersChar = separateEncryptingLettersChar;
    }
    public IDictionary<int, char> CalcualteAlphabetPolynomials()
    {
        var polynomials = Alphabet.Select(x => Polinomial(x.Value) % Mod);
        return polynomials.Zip(Alphabet.Select(x => x.Key)).ToDictionary(x => x.First, x => x.Second);
    }
}
