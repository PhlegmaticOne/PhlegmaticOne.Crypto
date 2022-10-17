using System.Collections;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Extensions;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Keys;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Functions;

public class AndFeistelFunction : IFeistelFunction
{
    public BitArray F(BitArray enterBlock, FeistelKey feistelKey)
    {
        return enterBlock.ImmutableAnd(feistelKey.Key);
    }
}
