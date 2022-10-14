using System.Numerics;
using System.Text;
using PhlegmaticOne.Crypto.Asymmetric.RSA.EncryptionData;
using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Core.Models;

namespace PhlegmaticOne.Crypto.Asymmetric.RSA;

public class RsaAlgorithm : CryptoAlgorithmBase<RsaEncryptionData>
{
    public override string Description => "RSA asymmetric algorithm";

    public override EncryptionResult<RsaEncryptionData> Encrypt(string textToEncrypt, RsaEncryptionData encryptionData)
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
        var encryptedText = result.ToString();
        return new(encryptionData, textToEncrypt, encryptedText, Description, GetType());
    }

    public override DecryptionResult Decrypt(EncryptionResult<RsaEncryptionData> encryptionResult)
    {
        var result = new StringBuilder();
        var secretKey = encryptionResult.EncryptionData.SecretKey;
        var alphabet = encryptionResult.EncryptionData.Alphabet;
        var encrypted = encryptionResult.EncryptedText
            .Split(encryptionResult.EncryptionData.SeparatingChar, StringSplitOptions.RemoveEmptyEntries);

        foreach (var encryptedLetter in encrypted)
        {
            var encryptedLetterCode = new BigInteger(Convert.ToDouble(encryptedLetter));
            var temp = BigInteger.Pow(encryptedLetterCode, (int)secretKey.FirstMagicNumber);

            var decodedLetterCode = temp % secretKey.SecondMagicNumber;

            var index = Convert.ToInt32(decodedLetterCode.ToString());

            var letter = alphabet.ConvertDigit(index);

            result.Append(letter);
        }

        var decrypted = result.ToString();
        return DecryptionResult.FromEncryptionResult(encryptionResult, decrypted);
    }
}