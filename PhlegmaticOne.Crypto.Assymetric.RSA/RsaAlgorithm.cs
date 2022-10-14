using PhlegmaticOne.Crypto.Assymetric.RSA.EncrypitionData;
using PhlegmaticOne.Crypto.Core.Base;
using System.Numerics;
using System.Text;

namespace PhlegmaticOne.Crypto.Assymetric.RSA;

public class RsaAlgorithm : ICryptoAlgorithm<RsaEncryptionData>
{
    public EncryptionResult<RsaEncryptionData> Encrypt(string textToEncrypt, RsaEncryptionData encryptionData)
    {
        var result = new StringBuilder();
        encryptionData.GenerateKeys();

        var publicKey = encryptionData.PublicKey;

        foreach (var letter in textToEncrypt)
        {
            var letterCode = encryptionData.Alphabet.ConvertLetter(letter);

            var b = BigInteger.Pow(letterCode, (int)publicKey.FirstMagicNumber);

            var encrypted = b % publicKey.SecondMagicNumber;

            result.Append(encrypted);
            result.Append(encryptionData.SeparatingChar);
        }

        return new EncryptionResult<RsaEncryptionData>(encryptionData, textToEncrypt, result.ToString());
    }

    public DecryptionResult Decrypt(EncryptionResult<RsaEncryptionData> encryptionResult)
    {
        var result = new StringBuilder();
        var secretKey = encryptionResult.EncyptionData.SecretKey;
        var alphabet = encryptionResult.EncyptionData.Alphabet;
        var encrypted = encryptionResult.EncryptedText
            .Split(encryptionResult.EncyptionData.SeparatingChar, StringSplitOptions.RemoveEmptyEntries);

        foreach (var encryptedLetter in encrypted)
        {
            var encryptedLetterCode = new BigInteger(Convert.ToDouble(encryptedLetter));
            var temp = BigInteger.Pow(encryptedLetterCode, (int)secretKey.FirstMagicNumber);

            var decodedLetterCode = temp % secretKey.SecondMagicNumber;

            var index = Convert.ToInt32(decodedLetterCode.ToString());

            var letter = alphabet.ConvertDigit(index);

            result.Append(letter);
        }

        return new(encryptionResult.OriginalText, result.ToString(), encryptionResult.EncryptedText);
    }
}
