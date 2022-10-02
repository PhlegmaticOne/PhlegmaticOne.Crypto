using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Symmetric.Polynomial.EncryptionData;
using System.Text;

namespace PhlegmaticOne.Crypto.Symmetric.Polynomial;

public class PolynomialAlgorithm : ICryptoAlgorithm<PolynomialAlgorithmEncryptionData>
{
    public EncryptionResult<PolynomialAlgorithmEncryptionData> Encrypt(string textToEncrypt, 
        PolynomialAlgorithmEncryptionData encryptionData)
    {
        var sb = new StringBuilder();
        foreach (var letter in textToEncrypt)
        {
            var encryptedLetter = GetEncryptedLetter(letter, encryptionData);
            sb.Append(encryptedLetter.ToString());
            sb.Append(encryptionData.SeparateEncryptingLettersChar);
        }

        return new EncryptionResult<PolynomialAlgorithmEncryptionData>(encryptionData, textToEncrypt, sb.ToString());
    }
    public DecryptionResult Decrypt(EncryptionResult<PolynomialAlgorithmEncryptionData> encryptionResult)
    {
        var sb = new StringBuilder();
        var encryptedLetters = encryptionResult.EncryptedText
            .Split(encryptionResult.EncyptionData.SeparateEncryptingLettersChar, StringSplitOptions.RemoveEmptyEntries);
        var alphabetPolynomials = encryptionResult.EncyptionData.CalcualteAlphabetPolynomials();

        foreach (var encryptedLetter in encryptedLetters)
        {
            var encryptedLetterNumber = Convert.ToInt32(encryptedLetter);

            var decodedLetter = alphabetPolynomials[encryptedLetterNumber];

            sb.Append(decodedLetter);
        }
        return new(encryptionResult.OriginalText, sb.ToString(), encryptionResult.EncryptedText);
    }
    private static int GetEncryptedLetter(char letter, PolynomialAlgorithmEncryptionData encryptionData)
    {
        var letterCode = encryptionData.Alphabet.ConvertLetter(letter);
        return encryptionData.Polinomial(letterCode) % encryptionData.Mod;
    }
}
