using PhlegmaticOne.Crypto.Symmetric.Feistel.Keys;
using System.Collections;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Functions;

public interface IFeistelFunction
{
    BitArray F(BitArray enterBlock, FeistelKey feistelKey);
}