using System.Collections;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Keys;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Functions;

public interface IFeistelFunction
{
    BitArray F(BitArray enterBlock, FeistelKey feistelKey);
}