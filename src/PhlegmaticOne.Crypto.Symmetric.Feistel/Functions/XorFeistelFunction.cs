using PhlegmaticOne.Crypto.Symmetric.Feistel.Extensions;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Keys;
using System.Collections;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Functions;

public class XorFeistelFunction : IFeistelFunction
{
    public BitArray F(BitArray enterBlock, FeistelKey feistelKey)
    {
        return enterBlock.ImmutableXor(feistelKey.Key);
    }
}
