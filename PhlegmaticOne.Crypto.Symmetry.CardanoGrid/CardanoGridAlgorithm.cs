using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Core.Models;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid.EncryptionData;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Grid;
using System.Text;

namespace PhlegmaticOne.Crypto.Symmetric.CardanoGrid;

public class CardanoGridAlgorithm : CryptoAlgorithmBase<CardanoGridAlgorithmEncryptionData>
{
    public override string Description => "Cardano grid algorithm. (Решетка Кардано)";

    public override EncryptionResult<CardanoGridAlgorithmEncryptionData> Encrypt(string textToEncrypt,
        CardanoGridAlgorithmEncryptionData encryptionData)
    {
        var textGrid = encryptionData.GenerateTextGrid(textToEncrypt);
        var mask = encryptionData.GenerateMask();
        var strings = textToEncrypt.Chunk(textGrid.KPow2).Select(x => new string(x)).ToList();

        if(strings.Count < 4)
        {
            while(strings.Count != 4)
            {
                strings.Add(textGrid.FillString(string.Empty));
            }
        }

        for (var i = 0; i < 4; i++)
        {
            textGrid.ApplyMask(strings[i], mask);
            mask.Rotate90Clockwise();
        }

        var encrypted = textGrid.ToView();
        return new(encryptionData, textToEncrypt, encrypted, Description);
    }
    public override DecryptionResult Decrypt(EncryptionResult<CardanoGridAlgorithmEncryptionData> encryptionResult)
    {
        var original = encryptionResult.OriginalText;
        var textGrid = encryptionResult.EncryptionData.TextGrid;
        var mask = encryptionResult.EncryptionData.Mask;

        var countsToRead = original.Chunk(textGrid.KPow2).Select(x => x.Length).ToList();
        var sb = new StringBuilder();
        foreach (var countToRead in countsToRead)
        {
            var read = textGrid.ReadByMask(mask, countToRead);
            mask.Rotate90Clockwise();
            sb.Append(read);
        }

        var decrypted = sb.ToString();
        return DecryptionResult.FromEncryptionResult(encryptionResult, decrypted);
    }
}
