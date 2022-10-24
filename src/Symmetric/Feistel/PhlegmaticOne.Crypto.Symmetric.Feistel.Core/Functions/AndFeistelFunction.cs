using System.Collections;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Extensions;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Keys;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Functions;

public class AndFeistelFunction : IFeistelFunction
{
    public BitArray F(BitArray enterBlock, FeistelKey feistelKey)
    {
        return enterBlock.ImmutableAnd(feistelKey.Key);
    }
}
