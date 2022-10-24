using System.Collections;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Extensions;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Keys;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Functions;

public class XorFeistelFunction : IFeistelFunction
{
    public BitArray F(BitArray enterBlock, FeistelKey feistelKey)
    {
        return enterBlock.ImmutableXor(feistelKey.Key);
    }
}
