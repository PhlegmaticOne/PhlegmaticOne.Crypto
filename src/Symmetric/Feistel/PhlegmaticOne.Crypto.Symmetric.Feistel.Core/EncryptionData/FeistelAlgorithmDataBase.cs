using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Functions;
using System.Collections;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Keys;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Core.EncryptionData;

public class FeistelAlgorithmDataBase : IEncryptionData
{
    private readonly IFeistelFunction _feistelFunction;
    private readonly List<FeistelKey> _keys;
    public int Rounds { get; }
    public int BlockSize { get; }
    public int BlocksCount { get; }

    public FeistelAlgorithmDataBase(
        IFeistelRoundKeysGenerator roundKeysGenerator,
        IFeistelFunction feistelFunction,
        int rounds = 16, int blockSize = 128, int blocksCount = 4)
    {
        _feistelFunction = feistelFunction;
        Rounds = rounds;
        BlockSize = blockSize;
        BlocksCount = blocksCount;

        _keys = roundKeysGenerator.GenerateRoundKeys(rounds, blockSize / blocksCount).ToList();
    }

    public FeistelKey GetRoundKey(int round) => _keys[round];
    public BitArray ApplyFunction(BitArray enterBlock, FeistelKey feistelKey) =>
        _feistelFunction.F(enterBlock, feistelKey);
}