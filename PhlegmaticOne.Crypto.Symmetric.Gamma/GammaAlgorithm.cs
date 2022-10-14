using System.Text;
using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Core.Models;
using PhlegmaticOne.Crypto.Symmetric.Gamma.EncryptionData;

namespace PhlegmaticOne.Crypto.Symmetric.Gamma;

public class GammaAlgorithm : CryptoAlgorithmBase<GammaAlgorithmEncryptionData>
{
    public override string Description => "Gamma algorithm. (Гаммирование)";

    public override EncryptionResult<GammaAlgorithmEncryptionData> Encrypt(string textToEncrypt,
        GammaAlgorithmEncryptionData encryptionData)
    {
        var sb = new StringBuilder();
        var key = encryptionData.GenerateKey(textToEncrypt.Length);
        foreach(var letterAndKey in textToEncrypt.Zip(key))
        {
            var letterCode = encryptionData.Alphabet.ConvertLetter(letterAndKey.First);
            var codedLetterCode = (letterCode + letterAndKey.Second) % encryptionData.Mod;
            var codedLetter = encryptionData.Alphabet.ConvertDigit(codedLetterCode);
            sb.Append(codedLetter);
        }
        var encrypted = sb.ToString();
        return new(encryptionData, textToEncrypt, encrypted, Description);
    }

    public override DecryptionResult Decrypt(EncryptionResult<GammaAlgorithmEncryptionData> encryptionResult)
    {
        var sb = new StringBuilder();
        var textToDecrypt = encryptionResult.EncryptedText;
        var keyUsed = encryptionResult.EncryptionData.Key;
        var alphabet = encryptionResult.EncryptionData.Alphabet;
        var mod = encryptionResult.EncryptionData.Mod;

        foreach (var letterAndKey in textToDecrypt.Zip(keyUsed))
        {
            var letterCode = alphabet.ConvertLetter(letterAndKey.First);
            var difference = letterCode - letterAndKey.Second;
            var codedLetterCode = (difference >= 0) ? difference : (mod + difference) % mod;
            var codedLetter = alphabet.ConvertDigit(codedLetterCode);
            sb.Append(codedLetter);
        }
        var decrypted = sb.ToString();
        return DecryptionResult.FromEncryptionResult(encryptionResult, decrypted);
    }
}
