using System.Text;
using PhlegmaticOne.Crypto.ClassicCrypto.DigitalCryptography.EncryptionData;
using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Core.Helpers;
using PhlegmaticOne.Crypto.Core.Models;

namespace PhlegmaticOne.Crypto.ClassicCrypto.DigitalCryptography;

public class DigitalCryptographyAlgorithm : CryptoAlgorithmBase<DigitalAlgorithmData>
{
    public override string Description => "Digital algorithm. (Цифровая система тайнописи)";

    public override EncryptionResult<DigitalAlgorithmData> Encrypt(string textToEncrypt, DigitalAlgorithmData encryptionData)
    {
        var result = new StringBuilder();
        foreach (var letter in textToEncrypt)
        {
            var encryptedLetter = encryptionData.LetterEncryptionPolicy.EncryptLetter(letter);
            result.Append(encryptedLetter);
            if (letter != CharConstants.SPACE)
            {
                result.Append(encryptionData.SeparatingEncryptedLettersChar);
            }
        }

        var encrypted = result.ToString();
        return new(encryptionData, textToEncrypt, encrypted, Description);
    }

    public override DecryptionResult Decrypt(EncryptionResult<DigitalAlgorithmData> encryptionResult)
    {
        var split = encryptionResult.EncryptedText
            .Split(encryptionResult.EncryptionData.SeparatingEncryptedLettersChar, CharConstants.SPACE);

        var result = new StringBuilder();
        foreach (var encrypted in split)
        {
            result.Append(encryptionResult.EncryptionData.LetterEncryptionPolicy.DecryptLetter(encrypted));
        }

        var decrypted = result.ToString().TrimEnd();
        return DecryptionResult.FromEncryptionResult(encryptionResult, decrypted);
    }
}