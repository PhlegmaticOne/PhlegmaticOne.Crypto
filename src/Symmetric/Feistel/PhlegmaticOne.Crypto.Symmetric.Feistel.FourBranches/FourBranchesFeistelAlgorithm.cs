using System.Collections;
using System.Text;
using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Core.Models;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Extensions;
using PhlegmaticOne.Crypto.Symmetric.Feistel.FourBranches.EncryptionData;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.FourBranches;

public class FourBranchesFeistelAlgorithm : CryptoAlgorithmBase<FourBranchesFeistelAlgorithmData>
{
    public override string Description => "Four branches feistel algotithm. (Сеть Фейстеля с четырьм ветвями)";

    public override EncryptionResult<FourBranchesFeistelAlgorithmData> Encrypt(string textToEncrypt,
        FourBranchesFeistelAlgorithmData encryptionData)
    {
        var encrypted = ProcessStringWithBlockProcessingStrategy(textToEncrypt, encryptionData, EncodeBlock);

        return new(encryptionData, textToEncrypt, 
            encrypted, Description, GetType());
    }

    public override DecryptionResult Decrypt(EncryptionResult<FourBranchesFeistelAlgorithmData> encryptionResult)
    {
        var encryptedText = encryptionResult.EncryptedText;
        var encryptionData = encryptionResult.EncryptionData;

        var decrypted = ProcessStringWithBlockProcessingStrategy(encryptedText, encryptionData, DecodeBlock);

        return DecryptionResult.FromEncryptionResult(encryptionResult, decrypted);
    }

    private static string DecodeBlock(BitArray block, FourBranchesFeistelAlgorithmData encryptionData) =>
        ProcessBlockWithStrategy(block, encryptionData, (round, data, blocks) =>
        {
            var roundKey = data.GetRoundKey(data.Rounds - round - 1);
            var changedBlock = data.ApplyFunction(blocks[3], roundKey);
            var result = changedBlock.ImmutableXor(blocks[0]);

            var copy = (blocks[3].Clone() as BitArray)!;
            blocks[3] = blocks[2];
            blocks[2] = blocks[1];
            blocks[1] = result;
            blocks[0] = copy;
        });

    private static string EncodeBlock(BitArray block, FourBranchesFeistelAlgorithmData encryptionData) =>
        ProcessBlockWithStrategy(block, encryptionData, (round, data, blocks) =>
        {
            var roundKey = data.GetRoundKey(round);
            var changedBlock = data.ApplyFunction(blocks[0], roundKey);
            var result = changedBlock.ImmutableXor(blocks[1]);

            var copy = (blocks[0].Clone() as BitArray)!;
            blocks[0] = result;
            blocks[1] = blocks[2];
            blocks[2] = blocks[3];
            blocks[3] = copy;
        });

    private static string ProcessStringWithBlockProcessingStrategy(string input,
        FourBranchesFeistelAlgorithmData encryptionData,
        Func<BitArray, FourBranchesFeistelAlgorithmData, string> strategy)
    {
        var encryptedString = new StringBuilder();
        var blocks = input.SplitToBlocks(encryptionData.BlockSize).ToList();

        blocks.ForEach(b =>
        {
            var encoded = strategy(b, encryptionData);
            encryptedString.Append(encoded);
        });

        return encryptedString.ToString();
    }

    private static string ProcessBlockWithStrategy(BitArray block, FourBranchesFeistelAlgorithmData data,
        Action<int, FourBranchesFeistelAlgorithmData, List<BitArray>> strategy)
    {
        var leftHalf = block.GetHalfBits(true);
        var rightHalf = block.GetHalfBits(false);

        var blocks = new List<BitArray>
        {
            leftHalf.GetHalfBits(true),
            leftHalf.GetHalfBits(false),
            rightHalf.GetHalfBits(true),
            rightHalf.GetHalfBits(false)
        };

        for (var i = 0; i < data.Rounds; i++)
        {
            strategy(i, data, blocks);
        }

        var leftBytes1 = blocks[0].ToByteArray();
        var leftBytes2 = blocks[1].ToByteArray();
        var rightBytes1 = blocks[2].ToByteArray();
        var rightBytes2 = blocks[3].ToByteArray();

        var encryptedBytes = leftBytes1.Concat(leftBytes2).Concat(rightBytes1).Concat(rightBytes2).ToArray();
        var encrypted = Encoding.Unicode.GetString(encryptedBytes);

        return encrypted;
    }
}