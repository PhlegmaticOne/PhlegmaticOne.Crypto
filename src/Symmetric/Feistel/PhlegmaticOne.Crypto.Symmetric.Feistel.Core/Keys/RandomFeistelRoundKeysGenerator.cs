using System.Collections;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Keys;

public class RandomFeistelRoundKeysGenerator : IFeistelRoundKeysGenerator
{
    public IEnumerable<FeistelKey> GenerateRoundKeys(int totalRounds, int keySizeInBits) =>
        Enumerable.Range(0, totalRounds)
            .Select(x =>
            {
                var bytes = new byte[keySizeInBits / 8];
                Random.Shared.NextBytes(bytes);
                var bitArray = new BitArray(bytes);
                return new FeistelKey(bitArray);
            });
}