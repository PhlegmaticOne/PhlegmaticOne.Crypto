using System.Collections;
using System.Text;
using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Core.Models;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Extensions;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Symmetric.EncryptionData;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Symmetric;

public class SymmetricFeistelAlgorithm : CryptoAlgorithmBase<SymmetricFeistelAlgorithmData>
{
    public override string Description => "Symmetric feistel algotithm. (Абсолютно симметричная сеть Фейстеля)";

    public override EncryptionResult<SymmetricFeistelAlgorithmData> Encrypt(string textToEncrypt, 
        SymmetricFeistelAlgorithmData encryptionData)
    {
        var encrypted = EncryptCommonPrivate(textToEncrypt, encryptionData);

        return new(encryptionData, textToEncrypt, 
            encrypted, Description, GetType());
    }

    public override DecryptionResult Decrypt(EncryptionResult<SymmetricFeistelAlgorithmData> encryptionResult)
    {
        var encryptedText = encryptionResult.EncryptedText;
        var encryptionData = encryptionResult.EncryptionData;

        var decrypted = EncryptCommonPrivate(encryptedText, encryptionData).TrimEnd('\0');

        return DecryptionResult.FromEncryptionResult(encryptionResult, decrypted);
    }

    private static string EncryptCommonPrivate(string source, SymmetricFeistelAlgorithmData encryptionData)
    {
        var blocks = source.SplitToBlocks(encryptionData.BlockSize);
        var encryptedString = new StringBuilder();

        foreach (var block in blocks)
        {
            var encoded = EncodeBlock(block, encryptionData);
            encryptedString.Append(encoded);
        }

        return encryptedString.ToString();
    }

    private static string EncodeBlock(BitArray block, SymmetricFeistelAlgorithmData encryptionData)
    {
        var leftBlock = block.GetHalfBits(true);
        var rightBlock = block.GetHalfBits(false);
        for (var i = 0; i < encryptionData.Rounds; i++)
        {
            var roundKey = encryptionData.GetRoundKey(i);
            var changedBlock = encryptionData.ApplyFunction(leftBlock, roundKey);
            var result = changedBlock.ImmutableXor(rightBlock);

            if (i != encryptionData.Rounds - 1)
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
