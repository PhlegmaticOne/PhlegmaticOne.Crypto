using System.Collections;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Functions;

public class PBoxFunction : IPostFeistelFunction
{
    public BitArray Process(BitArray inputBlock)
    {
        var result = new BitArray(inputBlock.Length);

        for (int i = inputBlock.Length - 1; i >= 0; i--)
        {
            result[inputBlock.Length - i - 1] = inputBlock[i];
        }

        return result;
    }
}
