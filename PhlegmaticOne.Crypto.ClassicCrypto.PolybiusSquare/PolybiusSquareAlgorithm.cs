using PhlegmaticOne.Crypto.ClassicCrypto.PolybiusSquare.EncryptionData;
using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Core.Models;

namespace PhlegmaticOne.Crypto.ClassicCrypto.PolybiusSquare;

public class PolybiusSquareAlgorithm : CryptoAlgorithmBase<PolybiusSquareEncryptionData>
{
    public override string Description => "Polybius algorithm. (Квадрат Полибия)";
    public override EncryptionResult<PolybiusSquareEncryptionData> Encrypt(string textToEncrypt,
        PolybiusSquareEncryptionData encryptionData)
    {
        var letterEncryptionPolicy = encryptionData.LetterEncryptionPolicy;
        letterEncryptionPolicy.PreEncryptAction(textToEncrypt);

        var encrypted = new string(textToEncrypt.SelectMany(letterEncryptionPolicy.EncryptLetter).ToArray());

        return new(encryptionData, textToEncrypt, encrypted, Description);
    }

    public override DecryptionResult Decrypt(EncryptionResult<PolybiusSquareEncryptionData> encryptionResult)
    {
        var letterEncryptionPolicy = encryptionResult.EncryptionData.LetterEncryptionPolicy;
        var encryptedText = encryptionResult.EncryptedText;
        letterEncryptionPolicy.PreDecryptAction(encryptedText);

        var decrypted = encryptedText.Select(x => letterEncryptionPolicy.DecryptLetter(x.ToString())).ToArray();
        var decryptedText = new string(decrypted);

        return DecryptionResult.FromEncryptionResult(encryptionResult, decryptedText);
    }
}
