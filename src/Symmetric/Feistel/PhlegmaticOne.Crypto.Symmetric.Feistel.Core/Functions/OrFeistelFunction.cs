using System.Collections;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Extensions;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Keys;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Functions;

public class OrFeistelFunction : IFeistelFunction
{
    public BitArray F(BitArray enterBlock, FeistelKey feistelKey)
    {
        return enterBlock.ImmutableOr(feistelKey.Key);
    }
}