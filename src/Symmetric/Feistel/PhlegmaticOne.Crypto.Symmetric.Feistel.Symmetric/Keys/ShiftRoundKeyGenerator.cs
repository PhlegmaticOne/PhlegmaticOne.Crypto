using PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Keys;
using System.Collections;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Extensions;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Symmetric.Keys;

public class ShiftRoundKeyGenerator : IFeistelRoundKeysGenerator
{
    public IEnumerable<FeistelKey> GenerateRoundKeys(int totalRounds, int keySizeInBits)
    {
        var initialKey = new byte[keySizeInBits / 8];
        Random.Shared.NextBytes(initialKey);
        var bitArray = new BitArray(initialKey);

        var halfKeys = Enumerable.Range(0, totalRounds / 2)
            .Select(x => bitArray.ImmutableCycleLeftShift(1)).ToList();
        var reversed = halfKeys.AsEnumerable().Reverse();

        return halfKeys.Concat(reversed).Select(x => new FeistelKey(x));
    }
}