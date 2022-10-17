using System.Collections;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Keys;

public class RandomInitialKeyGenerator : IInitialKeyGenerator
{
    public FeistelKey GenerateKey(int sizeInBits)
    {
        var bytes = new byte[sizeInBits / Constants.CHAR_SIZE_IN_BITS];
        Random.Shared.NextBytes(bytes);
        var bitArray = new BitArray(bytes);
        return new FeistelKey(bitArray);
    }
}