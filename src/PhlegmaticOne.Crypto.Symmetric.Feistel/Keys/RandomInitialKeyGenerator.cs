using System.Collections;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Keys;

public class RandomInitialKeyGenerator : IInitialKeyGenerator
{
    public FeistelKey GenerateKey(int sizeInBits)
    {
        var bytes = new byte[sizeInBits / Constants.CHAR_SIZE_IN_BITS];
        //bytes[0] = 1;
        //bytes[1] = 0;
        //bytes[2] = 1;
        //bytes[3] = 0;
        Random.Shared.NextBytes(bytes);
        var bitArray = new BitArray(bytes);
        return new FeistelKey(bitArray);
    }
}