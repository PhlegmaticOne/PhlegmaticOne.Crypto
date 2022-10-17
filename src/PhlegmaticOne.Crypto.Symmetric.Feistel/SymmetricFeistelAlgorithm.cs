using System.Collections;
using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Core.Models;
using PhlegmaticOne.Crypto.Symmetric.Feistel.EncryptionData;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Extensions;
using System.Text;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel;

public class SymmetricFeistelAlgorithm : CryptoAlgorithmBase<FeistelAlgorithmData>
{
    public override string Description => "Feistel algotithm. (Сеть Фейстеля)";

    public override EncryptionResult<FeistelAlgorithmData> Encrypt(string textToEncrypt, 
        FeistelAlgorithmData encryptionData)
    {
        var encrypted = EncryptCommonPrivate(textToEncrypt, encryptionData);

        return new(encryptionData, textToEncrypt, 
            encrypted, Description, GetType());
    }

    public override DecryptionResult Decrypt(EncryptionResult<FeistelAlgorithmData> encryptionResult)
    {
        var encryptedText = encryptionResult.EncryptedText;
        var encryptionData = encryptionResult.EncryptionData;

        var decrypted = EncryptCommonPrivate(encryptedText, encryptionData).TrimEnd('\0');

        return DecryptionResult.FromEncryptionResult(encryptionResult, decrypted);
    }

    private static string EncryptCommonPrivate(string source, FeistelAlgorithmData encryptionData)
    {
        var blocks = source.SplitToBlocks(encryptionData.BlockSizeInBits);
        var encryptedString = new StringBuilder();

        foreach (var block in blocks)
        {
            var encoded = EncodeBlock(block, encryptionData);
            encryptedString.Append(encoded);
        }

        return encryptedString.ToString();
    }

    private static string EncodeBlock(BitArray block, FeistelAlgorithmData encryptionData)
    {
        var leftBlock = block.GetHalfBits(true);
        var rightBlock = block.GetHalfBits(false);
        for (var i = 0; i < encryptionData.TotalRounds; i++)
        {
            var roundKey = encryptionData.GenerateRoundKey(i);
            var changedBlock = encryptionData.ApplyFunction(leftBlock, roundKey);
            //changedBlock = encryptionData.ApplyPostFunctions(changedBlock);
            var result = changedBlock.ImmutableXor(rightBlock);

            if (i != encryptionData.TotalRounds - 1)
            {
                rightBlock = leftBlock;
                leftBlock = result;
            }
            else
            {
                rightBlock = result;
            }
        }

        var leftBytes = leftBlock.ToByteArray();
        var rightBytes = rightBlock.ToByteArray();
        var encryptedBytes = leftBytes.Concat(rightBytes).ToArray();
        var encrypted = Encoding.Unicode.GetString(encryptedBytes);
        return encrypted;
    }
}
