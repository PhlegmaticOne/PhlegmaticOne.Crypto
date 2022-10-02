using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Symmetry.CardanoGrid.EncryptionData;

namespace PhlegmaticOne.Crypto.Symmetry.CardanoGrid;

public class CardanoGridAlgorithm : ICryptoAlgorithm<CardanoGridAlgorithmEncryptionData>
{
    public EncryptionResult<CardanoGridAlgorithmEncryptionData> Encrypt(string textToEncrypt, 
        CardanoGridAlgorithmEncryptionData encryptionData)
    {
        return new EncryptionResult<CardanoGridAlgorithmEncryptionData>(encryptionData, textToEncrypt, textToEncrypt);
    }
    public DecryptionResult Decrypt(EncryptionResult<CardanoGridAlgorithmEncryptionData> encryptionResult)
    {
        var indexesToSelect = encryptionResult.EncyptionData.Stencil.IndexesToSelect.ToList();
        var decrypted = encryptionResult.EncryptedText.Where((x, i) => indexesToSelect.Contains(i)).ToArray();
        var decryptedText = new string(decrypted);

        return new(encryptionResult.OriginalText, decryptedText, encryptionResult.EncryptedText);
    }
}
