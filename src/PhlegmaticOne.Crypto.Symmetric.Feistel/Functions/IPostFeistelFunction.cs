using System.Collections;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Functions;

public interface IPostFeistelFunction
{
    BitArray Process(BitArray inputBlock);
}
