using PhlegmaticOne.Crypto.Symmetric.Feistel.Core.EncryptionData;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Functions;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Keys;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Symmetric.EncryptionData;

public class SymmetricFeistelAlgorithmData : FeistelAlgorithmDataBase
{
    public SymmetricFeistelAlgorithmData(IFeistelRoundKeysGenerator roundKeysGenerator, IFeistelFunction feistelFunction,
        int rounds = 16, int blockSize = 128, int blocksCount = 4) :
        base(roundKeysGenerator, feistelFunction, rounds, blockSize, blocksCount)
    {
    }
}
