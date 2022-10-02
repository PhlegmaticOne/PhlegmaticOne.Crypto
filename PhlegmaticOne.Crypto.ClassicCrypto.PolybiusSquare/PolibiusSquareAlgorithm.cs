using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.PolybiusSquare.EncryptionData;

namespace PhlegmaticOne.Crypto.PolybiusSquare;

public class PolibiusSquareAlgorithm : ICryptoAlgorithm<PolybiusSquareEncryptionData>
{   
    public EncryptionResult<PolybiusSquareEncryptionData> Encrypt(string textToEncrypt,
        PolybiusSquareEncryptionData encryptionData)
    {
        var letterEncryptionPolicy = encryptionData.LetterEncryptionPolicy;
        letterEncryptionPolicy.PreEncryptAction(textToEncrypt);

        var encrypted = new string(textToEncrypt.SelectMany(letterEncryptionPolicy.EncryptLetter).ToArray());

        return new EncryptionResult<PolybiusSquareEncryptionData>(encryptionData, textToEncrypt, encrypted);
    }

    public DecryptionResult Decrypt(EncryptionResult<PolybiusSquareEncryptionData> encryptionResult)
    {
        var letterEncryptionPolicy = encryptionResult.EncyptionData.LetterEncryptionPolicy;
        var encryptedText = encryptionResult.EncryptedText;
        letterEncryptionPolicy.PreDecryptAction(encryptedText);

        var decrypted = encryptedText.Select(x => letterEncryptionPolicy.DecryptLetter(x.ToString())).ToArray();
        var decryptedText = new string(decrypted);

        return new(encryptionResult.OriginalText, decryptedText, encryptedText);
    }
}
