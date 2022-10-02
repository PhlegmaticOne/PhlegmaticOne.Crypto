using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Symmetric.Gamma.EncryptionData;
using System.Text;

namespace PhlegmaticOne.Crypto.Gamma;

public class GammaAlgorithm : ICryptoAlgorithm<GammaAlgorithmEncryptionData>
{
    public EncryptionResult<GammaAlgorithmEncryptionData> Encrypt(string textToEncrypt,
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

        return new EncryptionResult<GammaAlgorithmEncryptionData>(encryptionData, textToEncrypt, sb.ToString());
    }

    public DecryptionResult Decrypt(EncryptionResult<GammaAlgorithmEncryptionData> encryptionResult)
    {
        var sb = new StringBuilder();
        var textToDecrypt = encryptionResult.EncryptedText;
        var keyUsed = encryptionResult.EncyptionData.Key;
        var alphabet = encryptionResult.EncyptionData.Alphabet;
        var mod = encryptionResult.EncyptionData.Mod;

        foreach (var letterAndKey in textToDecrypt.Zip(keyUsed))
        {
            var letterCode = alphabet.ConvertLetter(letterAndKey.First);
            var difference = letterCode - letterAndKey.Second;
            var codedLetterCode = (difference >= 0) ? difference : (mod + difference) % mod;
            var codedLetter = alphabet.ConvertDigit(codedLetterCode);
            sb.Append(codedLetter);
        }

        return new(encryptionResult.OriginalText, sb.ToString(), textToDecrypt);
    }
}
