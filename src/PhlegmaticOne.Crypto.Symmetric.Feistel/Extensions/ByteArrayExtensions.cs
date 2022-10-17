using System.Collections;
using System.Text;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Extensions;

public static class ByteArrayExtensions
{
    public static IEnumerable<BitArray> SplitToBlocks(this string text, int blockSizeInBits) =>
        Encoding.Unicode.GetBytes(text)
            .Chunk(Constants.GetBytesCount(blockSizeInBits))
            .Select(x =>
            {
                if (x.Length >= Constants.GetBytesCount(blockSizeInBits)) return new BitArray(x);

                var result = new byte[Constants.GetBytesCount(blockSizeInBits)];
                for (var i = 0; i < x.Length; i++)
                {
                    result[i] = x[i];
                }
                return new BitArray(result);
            });
}