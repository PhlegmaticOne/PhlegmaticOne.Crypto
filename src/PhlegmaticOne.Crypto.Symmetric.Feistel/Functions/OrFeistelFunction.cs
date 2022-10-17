using System.Collections;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Extensions;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Keys;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Functions;

public class OrFeistelFunction : IFeistelFunction
{
    public BitArray F(BitArray enterBlock, FeistelKey feistelKey)
    {
        return enterBlock.ImmutableOr(feistelKey.Key);
    }
}