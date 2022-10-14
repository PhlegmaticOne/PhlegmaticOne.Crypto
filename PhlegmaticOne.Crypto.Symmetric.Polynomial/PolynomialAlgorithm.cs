using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Core.Models;
using PhlegmaticOne.Crypto.Symmetric.Polynomial.EncryptionData;
using System.Text;

namespace PhlegmaticOne.Crypto.Symmetric.Polynomial;

public class PolynomialAlgorithm : CryptoAlgorithmBase<PolynomialAlgorithmEncryptionData>
{
    public override string Description => "Polynomial algorithm. (Метод полиномов)";

    public override EncryptionResult<PolynomialAlgorithmEncryptionData> Encrypt(string textToEncrypt, 
        PolynomialAlgorithmEncryptionData encryptionData)
    {
        var sb = new StringBuilder();
        foreach (var letter in textToEncrypt)
        {
            var encryptedLetter = GetEncryptedLetter(letter, encryptionData);
            sb.Append(encryptedLetter.ToString());
            sb.Append(encryptionData.SeparateEncryptingLettersChar);
        }

        var encrypted = sb.ToString();
        return new(encryptionData, textToEncrypt, encrypted, Description);
    }
    public override DecryptionResult Decrypt(EncryptionResult<PolynomialAlgorithmEncryptionData> encryptionResult)
    {
        var sb = new StringBuilder();
        var encryptedLetters = encryptionResult.EncryptedText
            .Split(encryptionResult.EncryptionData.SeparateEncryptingLettersChar, StringSplitOptions.RemoveEmptyEntries);
        var alphabetPolynomials = encryptionResult.EncryptionData.CalculateAlphabetPolynomials();

        foreach (var encryptedLetter in encryptedLetters)
        {
            var encryptedLetterNumber = Convert.ToInt32(encryptedLetter);

            var decodedLetter = alphabetPolynomials[encryptedLetterNumber];

            sb.Append(decodedLetter);
        }

        var decrypted = sb.ToString();
        return DecryptionResult.FromEncryptionResult(encryptionResult, decrypted);
    }
    private static int GetEncryptedLetter(char letter, PolynomialAlgorithmEncryptionData encryptionData)
    {
        var letterCode = encryptionData.Alphabet.ConvertLetter(letter);
        return encryptionData.Polynomial(letterCode) % encryptionData.Mod;
    }
}
